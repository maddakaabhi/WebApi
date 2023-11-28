using ModelLayer.Models;
using RepositoryLayer.Entity;
using System.Collections.Generic;

namespace BusinessLayer.Interfaces
{
    public interface ILabelBusiness
    {
        LabelEntity AddLabel(LabelModel labelModel, int Userid);
        List<LabelEntity> GetLabels(int Userid);

        LabelEntity UpdateLabel(LabelModel labelModel, int Labelid, int Userid);

        bool DeleteLabel(int Labelid,int noteid,int Userid);

        
    }
}