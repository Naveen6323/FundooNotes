using BusinessLogicLayer;
using BusinessLogicLayer.Services;
using DataAcessLayer.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO;
using System.Drawing;
using System.Security.Claims;

namespace FundooNotes.Controllers
{
    [Route("api/authoriseduser")]
    [ApiController]
    [Authorize]

    public class AuthorizedUserControl : ControllerBase
    {
        private readonly IUserOperations userOperator;
        public AuthorizedUserControl(IUserOperations userOperator)
        {
            this.userOperator = userOperator;
        }

        [HttpGet("GetAllUsers")]
        public ActionResult<List<User>> GetAll()
        {
            userOperator.GetAllUsers();
            return Ok(userOperator.GetAllUsers());
        }

        [HttpPost("addnote")]
        public async Task<ActionResult> AddNotes(CreateNoteModel model)
        {
            try
            {
                var userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                //var em = User.FindFirstValue(ClaimTypes.Email);
                await userOperator.AddNote(model, userID);
                return Ok(new { message = "Note added successfully" });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
        [HttpGet("/getallnotes")]
        public async  Task<IActionResult> GetAllNotes()
        {
            
                var userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var res = await userOperator.GetAllNotes(userID);
                return Ok(res);
            

        }
        [HttpPut("notes/{id}")]
        public async Task<ActionResult> UpdateNoteById( int id, CreateNoteModel model)
        {
            if (model == null)
            {
                return BadRequest("Note model is null");
            }

            try
            {
                var userid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                await userOperator.UpdateNoteById(userid, id, model);
                return Ok(new {data="success"}); // 204 No Content
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message); // 404 Not Found
            }
        }
        [HttpGet("trashnotes")]
        public async Task<IActionResult> GetAllTrashNotes()
        {
            try
            {
                var userid =  int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var res = await userOperator.GetAllTrashedNotes(userid);
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(new {message=e.Message});
            }
        }
        [HttpGet("archievednotes")]
        public async Task<IActionResult> GetAllArcheivedNotes()
        {
            try
            {
                var userid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var res =  await userOperator.GetAllArchievedNotes(userid);
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
        [HttpGet("getnotebyid")]
        public async Task<ActionResult> GetNoteById(int id)
        {
            try
            {
                var userid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                return Ok(await userOperator.GetNoteById(userid, id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("deletenotebyid")]
        public async Task<ActionResult> DeleteNoteById(int id)
        {
            try
            {
                var userid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                 var res=await userOperator.DeleteNoteById(userid, id);
                return Ok(new {message=res});
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
        [HttpPut("archievenotebyid")]
        public async Task<ActionResult> ArcchieveNoteById(int id)
        {
            try
            {
                var userid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var res=await userOperator.ArchieveNotebyId(userid, id);
                return Ok(new { message = res });
            }
            catch (Exception e)
            {
                return BadRequest(new {message=e.Message});
            }
        }
        [HttpPut("changenotcolor")]
        public async Task<ActionResult> ChangeNoteColor(int id,string color)
        {
            try
            {
                var userid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var res = await userOperator.AddColorToNote(userid, id,color);
                return Ok(new { message = res });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
        [HttpPost("AddLabelToNote")]
        public async Task<IActionResult> AddLabelToNote(int noteid,string label)
        {
            try
            {
                var userid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                await userOperator.AddLabelToNote(userid, noteid,label);
                return Ok(new { message = "label added" });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
        [HttpDelete("DeleteLabelForNote")]
        public async Task<IActionResult> DeleteLabelForNote(int noteid, string label)
        {
            try
            {
                var userid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                await userOperator.DeleteLabelForNote(userid, noteid, label);
                return Ok(new { message = $"label deleted for note {noteid}" });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteLabelByID(int labelid)
        {
            try
            {
                var userid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                await userOperator.DeleteLabelByID(userid, labelid);
                return Ok(new { message = $"label deleted" });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }


    }
}
