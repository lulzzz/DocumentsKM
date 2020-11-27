using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text.Json;
using AutoMapper;
using DocumentsKM.Dtos;
using DocumentsKM.Models;
using DocumentsKM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DocumentsKM.Controllers
{
    [Route("api")]
    [Authorize]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class MarkLinkedDocsController : ControllerBase
    {
        private readonly IMarkLinkedDocService _service;
        private readonly IMapper _mapper;

        public MarkLinkedDocsController(
            IMarkLinkedDocService markLinkedDocService,
            IMapper mapper
        )
        {
            _service = markLinkedDocService;
            _mapper = mapper;
        }

        [HttpGet, Route("marks/{markId}/mark-linked-docs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<MarkLinkedDocResponse>> GetAllByMarkId(int markId)
        {
            var markLinkedDocs = _service.GetAllByMarkId(markId);
            return Ok(_mapper.Map<IEnumerable<MarkLinkedDocResponse>>(markLinkedDocs));
        }

        [HttpPost, Route("marks/{markId}/mark-linked-docs")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<MarkLinkedDoc> Add(int markId, MarkLinkedDocRequest markLinkedDocRequest)
        {
            Log.Information(JsonSerializer.Serialize(markLinkedDocRequest));
            try
            {
                var markLinkedDocModel = new MarkLinkedDoc{};
                _service.Add(markLinkedDocModel, markId, markLinkedDocRequest.LinkedDocId);
                return Created($"mark-linked-docs/", markLinkedDocModel);
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
            catch (ConflictException)
            {
                return Conflict();
            }
        }

        [HttpPatch, Route("mark-linked-docs/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult Update(int id, [FromBody] MarkLinkedDocRequest markLinkedDocRequest)
        {
            // DEBUG
            // Log.Information(JsonSerializer.Serialize(markRequest));
            try
            {
                _service.Update(id, markLinkedDocRequest);
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
            catch (ConflictException)
            {
                return Conflict();
            }
            return NoContent();
        }

        [HttpDelete, Route("mark-linked-docs/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return NoContent();
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
        }
    }
}