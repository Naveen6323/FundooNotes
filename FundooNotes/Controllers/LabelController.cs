using BusinessLogicLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO;
using System.Security.Claims;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LabelController : ControllerBase
    {
        private readonly ILabelOperations labelOperator;
        public LabelController(ILabelOperations labelOperations)
        {
            this.labelOperator = labelOperations;
        }
        //[HttpPost]
        //public async Task<ActionResult> AddLabel(int noteid, string labelname)
        //{
        //    try
        //    {
        //        var userid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        //        await labelOperator.AddLabel(userid, noteid, labelname);
        //        return Ok("Label added successfully");

        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}
        //[HttpGet]
        //public async Task<ActionResult> GetAllLabels()
        //{
        //    try
        //    {
        //        int userid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        //        var res=await labelOperator.GetAllLabels(userid);
        //        return Ok(res);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}
        //[HttpGet("{name}")]
        //public async Task<IActionResult> getallnoteidwithlabelname(string name)
        //{
        //    try
        //    {
        //        int userid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        //        var res = await labelOperator.GetAllNotesWithLableName(userid,name);
        //        return Ok(res);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }

        //}
        //[HttpDelete]
        //public async Task<ActionResult> DeleteLableByName(string name)
        //{
        //    try
        //    {
        //        int userid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        //        await labelOperator.DeleteLabelByName(name, userid);
        //        return Ok("label deleted successfull");
        //    }catch(Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}
    }
}
