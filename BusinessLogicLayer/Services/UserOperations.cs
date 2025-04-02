using DataAcessLayer.Context;
using DataAcessLayer.Entity;
//using DataAcessLayer.Migrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ModelLayer.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;

namespace BusinessLogicLayer.Services
{
    public class UserOperations:IUserOperations
    {
        private readonly UserDBContext user;
        public UserOperations(UserDBContext user)
        {
            this.user = user;
        }

        public List<User> GetAllUsers()
        {

            return user.Users.ToList();
        }
        public async Task AddNote(CreateNoteModel model, int userid)
        {
            var note = new UserNotes
            {
                Title = model.Title,
                Description = model.Description,
                Color = model.Color,
                IsArchieved = model.IsArchieved,
                IsTrash = model.IsTrash,
                UserID = userid

            };
            await user.Notes.AddAsync(note);
            await user.SaveChangesAsync();

        }
        public async Task<List<GetAllNotesWithLabelResponse>> GetAllNotes(int userid)
        {
            var notesWithLabels = await user.Notes
                            .Where(n => n.UserID == userid)
                            .Include(n => n.NoteLabels)
                            .Select(n => new GetAllNotesWithLabelResponse
                            {
                                NoteId = n.NoteId,
                                title= n.Title,
                                description = n.Description,
                                color=n.Color,
                                isArchieved=n.IsArchieved,
                                isTrash=n.IsTrash,
                                Labels = n.NoteLabels.Select(l => new NoteLabelDto
                                {
                                    LabelId = l.LabelId,
                                    labelname = l.Label.LabelName
                                }).ToList()
                            })
                            .ToListAsync();
           return notesWithLabels;
        }



        public async Task UpdateNoteById(int userid, int id, CreateNoteModel model)
        {
            var note = await user.Notes.Where(x => x.UserID == userid && x.NoteId == id).FirstOrDefaultAsync();
            if (note != null)
            {
                note.Title = model.Title;
                note.Description = model.Description;
                note.Color = model.Color;
                note.IsArchieved = model.IsArchieved;
                note.IsTrash = model.IsTrash;
                await user.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Note not found");
            }
        }
        public async Task<List<UserNotes>> GetAllTrashedNotes(int userid)
        {
            var notes = await user.Notes.Where(x => x.UserID == userid && x.IsTrash == true && x.IsArchieved==false).ToListAsync();
            if (notes != null)
            {
                return notes;
            }
            else
            {
                throw new Exception("No trashed notes found");
            }
        }
        public async Task<List<UserNotes>> GetAllArchievedNotes(int userid)
        {
            var notes = await user.Notes.Where(x => x.UserID == userid && x.IsArchieved == true && x.IsTrash==false).ToListAsync();
            if (notes != null)
            {
                return notes;
            }
            else
            {
                throw new Exception("No archieved notes found");
            }
        }
        public async Task<NotesWithLabelNameModel> GetNoteById(int userid, int id)
        {
            var note = await user.Notes.Where(x => x.UserID == userid && x.NoteId == id).FirstOrDefaultAsync();
            if (note != null)
            {
                var model = await user.Notes
                            .Where(n => n.UserID == userid && n.NoteId == id)
                            .Include(n => n.NoteLabels)
                            .Select(n => new NotesWithLabelNameModel
                            {
                                noteid = n.NoteId,
                                title = n.Title,
                                description = n.Description ?? string.Empty, // Fix for CS8601
                                labelname = n.NoteLabels.Select(l => l.Label.LabelName).ToList()
                            })
                            .FirstOrDefaultAsync();

                return model;
            }
            else
            {
                throw new Exception("Note not found");
            }
        }
        public async Task<string> DeleteNoteById(int userid, int id)
        {
            var note = await user.Notes.Where(Notes => Notes.UserID == userid && Notes.NoteId == id).FirstOrDefaultAsync();
            if (note == null)
            {
                throw new Exception("Note not found");
            }
            else if (note.IsTrash == true)
            {
                var notelabel = await user.NoteLabels.Where(n => n.NoteId == id).ToListAsync();
                if (notelabel == null)
                {
                    user.Notes.Remove(note);
                    await user.SaveChangesAsync();
                    return "deleted";
                }
                else
                {
                    foreach (var item in notelabel)
                    {
                        user.NoteLabels.Remove(item);
                        await user.SaveChangesAsync();
                    }
                    user.Notes.Remove(note);
                    await user.SaveChangesAsync();
                    return "deleted";
                }
            }
            else
            {
                note.IsTrash = true;
                await user.SaveChangesAsync();
                return "trashed";
            }
        }
        public async Task<string> ArchieveNotebyId(int userid,int id)
        {
            var note = await user.Notes.Where(Notes => Notes.UserID == userid && Notes.NoteId == id).FirstOrDefaultAsync();
            if (note == null)
            {
                throw new Exception("Note not found");
            }
            else if (note.IsArchieved == true)
            {
                note.IsArchieved = false;
                await user.SaveChangesAsync();
                return "not unArchived successfully";
            }
            else
            {
                note.IsArchieved = true;
                await user.SaveChangesAsync();
                return "not archived";
            }
        }
        public async Task<string> AddColorToNote(int userid,int id,string color)
        {
            var note = await user.Notes.Where(Notes => Notes.UserID == userid && Notes.NoteId == id).FirstOrDefaultAsync();
            if (note == null)
            {
                throw new Exception("Note not found");
            }
            else
            {
                note.Color = color;
                await user.SaveChangesAsync();
                return color;
            }
        }
        public async Task AddLabelToNote(int userid, int noteid, string label)
        {
            var note = await user.Notes.Where(n => n.UserID == userid && n.NoteId == noteid).FirstOrDefaultAsync();
            if (note == null)
            {
                throw new Exception("note not found");
            }
            else
            {
                var existingLabel=await user.Labels.Where(x=>x.LabelName.Equals(label)).FirstOrDefaultAsync();
                if (existingLabel==null)
                {
                    var l = new Label
                    {
                        LabelName= label,
                        UserId= userid
                    };
                    await user.Labels.AddAsync(l);
                    await user.SaveChangesAsync();
                    var newlabel = await user.Labels.Where(x => x.LabelName.Equals(label)).FirstOrDefaultAsync();
                    if (newlabel == null) {
                        throw new Exception("label not added");
                    }
                    else
                    {
                        var notelabel = new NoteLabel
                        {
                            NoteId = noteid,
                            LabelId = newlabel.LabelId
                        };
                        await user.NoteLabels.AddAsync(notelabel);
                        await user.SaveChangesAsync();
                    }
                        
                }
                else
                {
                    var existnotelabel = await GetNoteById(userid, noteid);
                    if (existnotelabel.labelname.Contains(label))
                    {
                        throw new Exception("label already assciated");
                    }
                    else
                    {
                        var noteabel = new NoteLabel
                        {
                            LabelId = existingLabel.LabelId,
                            NoteId = noteid
                        };
                        await user.NoteLabels.AddAsync(noteabel);
                        await user.SaveChangesAsync();
                    }
                        
                }
            }
        }

        public async Task<string> DeleteLabelByID(int userid,int labelid)
        {
            var label= await user.Labels.Where(x=>x.UserId==userid && x.LabelId==labelid).FirstOrDefaultAsync();
            if (label == null)
            {
                throw new Exception("label not found");
            }
            else
            {
                var notelabel= await user.NoteLabels.Where(x=>x.LabelId==labelid).ToListAsync();
                if (notelabel == null) 
                {
                    user.Labels.Remove(label);
                    await user.SaveChangesAsync();
                    return "label deleted";
                }
                else
                {
                    foreach (var item in notelabel)
                    {
                        user.NoteLabels.Remove(item);
                        await user.SaveChangesAsync();
                    }
                    user.Labels.Remove(label);
                    await user.SaveChangesAsync();
                    return "label deleted";
                }

            }
        }
        public async Task DeleteLabelForNote(int userid, int noteid, string label)
        {
           
            var labelid = await user.Labels.Where(x => x.LabelName == label).FirstOrDefaultAsync();
            if (labelid == null)
            {
                throw new Exception("label not exist");
            }
            else
            {
                var notelabel = await user.NoteLabels.Where(x => x.NoteId == noteid && x.LabelId == labelid.LabelId).FirstOrDefaultAsync();
                if (notelabel == null)
                {
                    throw new Exception("label not found");
                }
                else
                {
                    user.NoteLabels.Remove(notelabel);
                    await user.SaveChangesAsync();
                }
            }
            
        }

    }
}
