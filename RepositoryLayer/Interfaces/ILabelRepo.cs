using ModelLayer.Models;
using RepositoryLayer.Entity;
using System.Collections.Generic;

namespace RepositoryLayer.Interfaces
{
    public interface ILabelRepo
    {
        LabelEntity AddLabel(LabelModel labelModel, int Userid);
        List<LabelEntity> GetLabels(int Userid);
    }
}