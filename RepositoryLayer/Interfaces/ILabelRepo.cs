using ModelLayer.Models;
using RepositoryLayer.Entity;
using System.Collections.Generic;

namespace RepositoryLayer.Interfaces
{
    public interface ILabelRepo
    {
        LabelEntity AddLabel(LabelModel labelModel, int Userid);
        List<LabelEntity> GetLabels(int Userid, int noteid);

        LabelEntity UpdateLabel(LabelModel labelModel,int Labelid, int Userid);
  
        bool DeleteLabel(int Labelid, int noteid, int userId);
    }
}