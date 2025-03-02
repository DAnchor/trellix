namespace Trellix.Services.Container.Api;

public static class Routes
{
    private const string Base = "api";

    public static class Attachment
    {
        private const string NameOfController = nameof(Attachment);
        public const string CreateAttachment = Base + "/" + NameOfController + "/CreateAttachment";
        public const string DownloadAttachment = Base + "/" + NameOfController + "/DownloadAttachment?id={id}";
        public const string GetAttachments = Base + "/" + NameOfController + "/GetAttachments";
    }
};