using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeApi.Services;

namespace RecipeApi.Controllers
{
    [Authorize(Policy = "CanWorkWithRecipe")]
    [Produces("application/jsosn")]
    [Route("api/recipe-qr-codes")]
    public class RecipeQrCodesController : Controller
    {
        private readonly QrMailSender _qrMailSender;

        private readonly QrCodeService _qrCodeService;

        public RecipeQrCodesController(QrMailSender qrMailSender,QrCodeService qrCodeService)
        {
            this._qrMailSender = qrMailSender;
            this._qrCodeService = qrCodeService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]QrSendInfo qrInfo)
        {
            if (qrInfo == null)
                return new StatusCodeResult(404);

            var image = await this._qrCodeService.GetQrImage(qrInfo.RecipeId);

            if (image == null)
                return new StatusCodeResult(404);

            await this._qrMailSender.SendAsync(qrInfo.Email, qrInfo.RecipeId, image);

            return Ok();
        }
    }
}