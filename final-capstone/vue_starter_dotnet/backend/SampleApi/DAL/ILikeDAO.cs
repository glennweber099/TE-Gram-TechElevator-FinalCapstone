using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.DAL
{
    public interface ILikeDAO
    {
        void ToggleLike(int photoId, int userId);
    }
}
