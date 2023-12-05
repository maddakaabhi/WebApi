using ModelLayer.Models;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class LabelRepo : ILabelRepo
    {
        private readonly NotesDBContext notesDBContext;
        public LabelRepo(NotesDBContext notesDBContext)
        {
            this.notesDBContext = notesDBContext;
        }
        public LabelEntity AddLabel(LabelModel labelModel, int Userid)
        {
            var result= notesDBContext.LabelsT.Where(x=>x.UserId==Userid && x.Id==labelModel.Id).ToList();
            if (result !=null)
            {
                var result1=result.Find(y=>y.LabelName==labelModel.LabelName);
                if (result1 == null)
                {
                    LabelEntity label = new LabelEntity();
                    label.Id = labelModel.Id;
                    label.LabelName = labelModel.LabelName;
                    label.UserId = Userid;
                    label.UpdatedAt = DateTime.Now;
                    label.CreatedAt = DateTime.Now;
                    notesDBContext.LabelsT.Add(label);
                    notesDBContext.SaveChanges();
                    return label;

                }
                else
                {
                    return null;
                }
               
            }
            else
            {
                return null;
            }
        }

        public List<LabelEntity> GetLabels(int Userid,int noteid)
        {
            List<LabelEntity> labelEntities = notesDBContext.LabelsT.Where(x=>x.UserId==Userid && x.Id==noteid).ToList();
            if(labelEntities != null)
            {
                return labelEntities;
            }
            else
            {
                return null;
            }

        }

        public LabelEntity UpdateLabel(LabelModel labelModel,int Labelid, int Userid)
        {
            var result = notesDBContext.LabelsT.FirstOrDefault(x=>x.UserId==Userid&&x.Id==labelModel.Id&&x.LabelId==Labelid);
            if(result != null)
            {
                result.LabelName = labelModel.LabelName;
                result.UpdatedAt = DateTime.Now;
                notesDBContext.SaveChanges();
                return result;
            }
            else
            {
                return null;
            }
        }

        public bool DeleteLabel(int Labelid,int noteid,int userId) 
        {
            var result = notesDBContext.LabelsT.ToList().Find(x => x.LabelId == Labelid && x.Id == noteid && x.UserId == userId);
            if (result != null)
            {
                notesDBContext.LabelsT.Remove(result);
                notesDBContext.SaveChanges();
                return true; 
            }
            else
            {
                return false;
            }
        }
       
    }
}
