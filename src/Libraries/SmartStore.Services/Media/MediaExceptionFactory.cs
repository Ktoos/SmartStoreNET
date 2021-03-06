﻿using System;
using SmartStore.Core.Localization;
using SmartStore.Utilities;

namespace SmartStore.Services.Media
{
    #region Exception classes

    public sealed class MediaFileNotFoundException : SmartException
    {
        public MediaFileNotFoundException(string message) : base(message) { }
    }

    public sealed class MediaFolderNotFoundException : SmartException
    {
        public MediaFolderNotFoundException(string message) : base(message) { }
    }

    public sealed class DuplicateMediaFileException : SmartException
    {
        public DuplicateMediaFileException(string message, MediaFileInfo dupeFile) : base(message)
        {
            File = dupeFile;
        }

        public MediaFileInfo File { get; }
    }

    public sealed class DuplicateMediaFolderException : SmartException
    {
        public DuplicateMediaFolderException(string message, MediaFolderNode dupeFolder) : base(message)
        {
            Folder = dupeFolder;
        }

        public MediaFolderNode Folder { get; }
    }

    public sealed class NotSameAlbumException : SmartException
    {
        public NotSameAlbumException(string message) : base(message) { }
    }

    public sealed class DeniedMediaTypeException : SmartException
    {
        public DeniedMediaTypeException(string message) : base(message) { }
    }

    public sealed class ExtractThumbnailException : SmartException
    {
        public ExtractThumbnailException(string message) : base(message) { }
        public ExtractThumbnailException(string message, Exception innerException) : base(message, innerException) { }
    }

    public sealed class MaxMediaFileSizeExceededException : SmartException
    {
        public MaxMediaFileSizeExceededException(string message) : base(message) { }
    }

    #endregion

    public class MediaExceptionFactory
    {
        public Localizer T { get; set; } = NullLocalizer.Instance;

        public MediaFileNotFoundException FileNotFound(string path)
        {
            return new MediaFileNotFoundException(T("Admin.Media.Exception.FileNotFound", "<b>" + path + "</b>"));
        }

        public MediaFolderNotFoundException FolderNotFound(string path)
        {
            return new MediaFolderNotFoundException(T("Admin.Media.Exception.FolderNotFound", "<b>" + path + "</b>"));
        }

        public DuplicateMediaFileException DuplicateFile(string fullPath, MediaFileInfo dupeFile)
        {
            return new DuplicateMediaFileException(T("Admin.Media.Exception.DuplicateFile", "<b>" + fullPath + "</b>"), dupeFile);
        }

        public DuplicateMediaFolderException DuplicateFolder(string fullPath, MediaFolderNode dupeFolder)
        {
            return new DuplicateMediaFolderException(T("Admin.Media.Exception.DuplicateFolder", "<b>" + fullPath + "</b>"), dupeFolder);
        }

        public NotSameAlbumException NotSameAlbum(string sourcePath, string destPath)
        {
            return new NotSameAlbumException(T("Admin.Media.Exception.NotSameAlbum", "<b>" + sourcePath + "</b>", "<b>" + destPath + "</b>"));
        }

        public DeniedMediaTypeException DeniedMediaType(string fileName, string currentType, string[] acceptedTypes = null)
        {
            var msg = T("Admin.Media.Exception.DeniedMediaType", "<b>" + fileName + "</b>", "<b>" + currentType + "</b>");
            if (acceptedTypes != null && acceptedTypes.Length > 0)
            {
                var types = string.Join(", ", acceptedTypes);
                msg += T("Admin.Media.Exception.DeniedMediaType.Hint", types, currentType);
            }

            return new DeniedMediaTypeException(msg);
        }

        public ExtractThumbnailException ExtractThumbnail(string path, string reason = null)
        {
            return new ExtractThumbnailException(T("Admin.Media.Exception.ExtractThumbnail", "<b>" + path + "</b>", reason.NaIfEmpty()));
        }

        public ExtractThumbnailException ExtractThumbnail(string path, Exception innerException)
        {
            Guard.NotNull(innerException, nameof(innerException));
            return new ExtractThumbnailException(T("Admin.Media.Exception.ExtractThumbnail", "<b>" + path + "</b>", innerException.Message), innerException);
        }

        public MaxMediaFileSizeExceededException MaxFileSizeExceeded(string fileName, long fileSize, long maxSize)
        {
            return new MaxMediaFileSizeExceededException(T(
                "Admin.Media.Exception.MaxFileSizeExceeded",
                "<b>" + fileName.NaIfEmpty() + "</b>",
                "<b>" + Prettifier.BytesToString(fileSize) + "</b>",
                "<b>" + Prettifier.BytesToString(maxSize)) + "</b>"
                );
        }
    }
}
