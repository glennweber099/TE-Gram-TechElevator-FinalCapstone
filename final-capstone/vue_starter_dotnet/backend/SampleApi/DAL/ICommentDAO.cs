using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.DAL
{
    /// <summary>
    /// An interface associated with the Comments data
    /// </summary>
    public interface ICommentDAO
    {
        void GetAllcommentsByPhotoId(int photoId);
    }
}
