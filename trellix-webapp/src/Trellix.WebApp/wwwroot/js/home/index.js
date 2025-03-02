$(window).on("DOMContentLoaded", function () {
    const indexPromise = new Promise((resolve) => {
        setTimeout(() => { resolve(document) }, 10);
    });

    const attachmentsPromise = new Promise((resolve) => {
        setTimeout(() => { resolve(indexPromise) }, 777);
    });

    $("#x-close-add-modal").on("click", function () {
        $("#x-form-input").val("");
        $("#x-add-modal").modal("hide");
    });

    $("#x-add-modal").on("hide.bs.modal", function () {
        $("#x-form-input").val("");
    });

    $("#x-add-attachment").on("click", function (event) {
        event.preventDefault();

        $("#x-add-modal").modal("show");

        $("#x-add-modal-button").on("click", async function () {
            let addAttachmentButton = $(this);
            addAttachmentButton.attr("disabled", true);

            let formData = new FormData();
            let file = $("#x-form-input").get(0).files;

            if (file[0].name.length) {
                let nameWithNoExtension = file[0].name
                    .substring(0, file[0].name.lastIndexOf(".")) || file[0].name;

                formData.append("Name", nameWithNoExtension);
                formData.append("Data", file[0]);

                const options = {
                    method: "POST",
                    mode: "same-origin",
                    headers: {
                        "Access-Control-Allow-Origin": window.location.origin
                    },
                    body: formData
                }

                await fetch(`${window.location.origin}/Attachment/CreateAttachment`, options)
                    .then(response => response.json()
                    .then(data => ({ status: response.status, responseAttachment: data })))
                    .then(response => {
                        console.log(response.responseAttachment);

                        if (response.responseAttachment.statusCode === 201) {
                            addAttachmentButton.removeAttr("disabled");
                            
                            $("#x-response-message").append(
                                `<span class="text-success">${response.responseAttachment.message}</span>`
                            );

                            setTimeout(() => {
                                $("#x-response-message").html("");
                                window.location.reload();
                            }, 5000);
                        }

                        if (response.responseAttachment.statusCode !== 201) {
                            addAttachmentButton.removeAttr("disabled");

                            $("#x-response-message").append(
                                `<span class="text-danger">${response.responseAttachment.message}</span>`
                            );

                            setTimeout(() => {
                                $("#x-response-message").html("");
                            }, 5000);
                        }
                    }).catch(error => {
                        addAttachmentButton.removeAttr("disabled");
                        $("#x-response-message").append(
                            `<span class="text-danger">${error}</span>`
                        );

                        setTimeout(() => {
                            $("#x-response-message").html("");
                        }, 5000);
                    });
            }
        });

    });

    const getAttachments = async function () {
        const url = `${window.location.origin}/Attachment/GetAttachments`;
    
        await fetch(url, {
            method: "GET",
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json"
            }
        })
        .then(response => response.json()
        .then(data => ({ status: response.status, responseAttachmentList: data })))
        .then(response => {
            if (response.status === 200) {
                setTimeout(() => {
                    $("tbody").children().remove();

                    response.responseAttachmentList.attachments.map((a, i) => {
                        $("tbody").append
                        (
                            `<tr>
                                <th scope="row">${a.id}</th>
                                <td>${a.name}</td>
                                <td>${new Date(a.createdOn).toLocaleDateString("en-US")}</td>
                                <td>${new Date(a.modifiedOn).toLocaleDateString("en-US")}</td>
                                <td class="icon-cell">
                                    <a href="#" class="action-button-link link-dark" id="x-entity-counter-${i}" x-prop-id="${a.id}" x-prop-file-name="${a.name}" title="Download Attachment: ${a.name}">
                                        <i class="bi bi-download h6"></i>
                                    </a>
                                </td>
                            </tr>`
                        );
                    });
                }, 1);
            }
        }).catch(error => console.log(error));
    };

    const attachmentDownload = async function () {
        $("[id^='x-entity-counter-']").on("click", async function (event) {
            event.preventDefault();
            console.log("click");

            let url = `${window.location.origin}/Attachment/DownloadAttachment?id=`;

            const options = {
                method: "GET",
                mode: "same-origin",
                headers: {
                    "Content-Type": "application/json",
                    "Access-Control-Allow-Origin": window.location.origin
                }
            };

            const fileOptions = function (url, propFileName) {
                let downloadLink = document.createElement("a");
                downloadLink.href = encodeURI(url);
                downloadLink.setAttribute("download", propFileName);
                downloadLink.click();
                downloadLink.remove();
            }

            const fetchPdf = async function (url, id, propFileName) {
                return await fetch(`${url + id}`, options)
                    .then(response => { return response.status === 200 ? response : null; })
                    .then(response => response.blob())
                    .then(blob => {
                        let completedDownloadOperation = function () {
                            let fileUrl = window.URL.createObjectURL(blob);
                            fileOptions(fileUrl, propFileName);
                        }
                        return blob === null ? console.log("Error getting the blob.") : completedDownloadOperation();
                    })
                    .catch(error => console.log(error));
            }

            return await fetchPdf(
                url,
                $(this).attr("x-prop-id"),
                $(this).attr("x-prop-file-name")
            );
        });
    };

    indexPromise
        .then(setTimeout(async () => {
            await getAttachments();
        }, 11))
        .catch(error => console.log(error));

    attachmentsPromise
        .then(setTimeout(async () => {
            await attachmentDownload();
        }, 777))
        .catch(error => console.log(error));
});