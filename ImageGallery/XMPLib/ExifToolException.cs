using System;
namespace ImageGallery.XMPLib
{
    [Serializable]
    public class ExifToolException : Exception
    {
        public ExifToolException(string msg) : base(msg)
        { }
    }
}
