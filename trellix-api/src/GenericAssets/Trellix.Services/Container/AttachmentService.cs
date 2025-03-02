namespace Trellix.Services.Container;

using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Trellix.Core.Models;
using Trellix.Dtos.Payload.Attachment;
using Trellix.Dtos.Request.Attachment;
using Trellix.Dtos.Response.Attachment;
using Trellix.Repositories.Crud;
using Trellix.Services.Exceptions;

public interface IAttachmentService
{
    Task<CreateAttachmentResponse> CreateAttachment(CreateAttachmentDto request);
    Task<DownloadAttachmentResponse> DownloadAttachment(DownloadAttachmentRequest request);
    Task<GetAttachmentsResponse> GetAttachments();
}

public class AttachmentService(IMapper mapper, IUnitOfWork unitOfWork) : IAttachmentService
{
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<CreateAttachmentResponse> CreateAttachment(CreateAttachmentDto request)
    {
        try
        {
            var iFormFileMapToByte = FormFileMapToByte(request.Data);

            var attachmentDto = new AttachmentDto
            (
                Name: request.Name,
                Data: iFormFileMapToByte
            );

            var attachmentModel = _mapper.Map<AttachmentDto, AttachmentModel>(attachmentDto);

            await _unitOfWork.Attachments.AddAsync(attachmentModel);

            await _unitOfWork.CompleteAsync();

            return new CreateAttachmentResponse
            (
                Attachment: attachmentDto,
                Success: true
            );
        }
        catch (ServiceException se)
        {
            throw new ServiceException(se.Message, se);
        }
    }

    public async Task<DownloadAttachmentResponse> DownloadAttachment(DownloadAttachmentRequest request)
    {
        try
        {
            var response = await _unitOfWork.Attachments
                .GetAttachmentByIdAsync(new Guid(request.Id));

            return new DownloadAttachmentResponse
            (
                AttachmentName: response.Name,
                AttachmentData: new MemoryStream(response.Data),
                Success: true
            );
        }
        catch (ServiceException se)
        {
            throw new ServiceException($"Unable to download attachment with Id {request.Id}:\n{se.Message}", se);
        }
    }

    public async Task<GetAttachmentsResponse> GetAttachments()
    {
        try
        {
            var response = await _unitOfWork.Attachments.FindAllAsync();
            var attachmentList = new List<AttachmentBasicDto>();

            foreach(var x in response)
            {
                attachmentList.Add(_mapper.Map<AttachmentModel, AttachmentBasicDto>(x));
            }

            return new GetAttachmentsResponse
            (
                Attachments: attachmentList,
                Success: true
            );

        }
        catch (ServiceException ex)
        {
            throw new ServiceException(string.Format("Get system attachments failed: {0}", ex.Message), ex);
        }
    }

    public byte[] FormFileMapToByte(IFormFile incomingFile)
    {
        if (incomingFile.Length != 0)
        {
            using var stream = new MemoryStream();

            incomingFile.CopyTo(stream);

            return stream.ToArray();
        }
        
        return [];
    }
}

