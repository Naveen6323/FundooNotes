using DataAcessLayer.Context;
using DataAcessLayer.Entity;
//using DataAcessLayer.Migrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ModelLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class LabelOperations : ILabelOperations
    {
        private readonly UserDBContext user;
        public LabelOperations(UserDBContext user)
        {
            this.user = user;
        }
        //public async Task AddLabel(int userid, string labelName)
        //{
            
        //    var labelname = new Label
        //    {
        //        LabelName = labelName
        //    };
        //    await user.Labels.AddAsync(labelname);
        //    await user.SaveChangesAsync();

        //}
        //public async Task<List<string>> GetAllLabels(int userid)
        //{
        //    var labelNames = from label in user.Labels
        //                     join note in user.Notes on label.noteId equals note.NoteId
        //                     join user in user.Users on note.UserID equals user.ID
        //                     where user.ID == userid
        //                     group label by label.LabelName into groupedLabels
        //                     select groupedLabels.Key;
        //    if (!labelNames.Any())
        //    {
        //        throw new Exception("No labels found");
        //    }

        //    return labelNames.ToList();
        //}
        //public async Task<List<int>> GetAllNotesWithLableName(int userid,string name)
        //{
        //    var res = from label in user.Labels
        //              join note in user.Notes on label.noteId equals note.NoteId into labelnotes
        //              from note in labelnotes.DefaultIfEmpty()
        //              join user in user.Users on note.UserID equals user.ID
        //              where user.ID == userid && label.LabelName==name
        //              select note.NoteId;
        //    if (!res.Any())
        //    {
        //        throw new Exception("no notes with lable name");
        //    }
        //    return res.ToList();
        //}
        //public async Task DeleteLabelByName(string name,int userid)
        //{
        //    var res = from label in user.Labels
        //              join note in user.Notes on label.noteId equals note.NoteId into labelnotes
        //              from note in labelnotes.DefaultIfEmpty()
        //              join user in user.Users on note.UserID equals user.ID
        //              where user.ID == userid && label.LabelName == name
        //              select label;
        //    if (!res.Any())
        //    {
        //        throw new Exception("no lables with that name");
        //    }
        //    foreach (var item in res)
        //    {

        //         user.Labels.Remove(item);
        //    }
        //    await user.SaveChangesAsync();
        //    return;

        //}





    }
}
