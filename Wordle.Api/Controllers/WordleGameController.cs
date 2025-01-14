using Microsoft.AspNetCore.Mvc;
using Wordle.Api.DTOs;
using Wordle.Api.Services;

namespace Wordle.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WordleGameController : ControllerBase
{
    private readonly WordFetcherService _service = new();

    [HttpGet("getWord")]
    public async Task<ActionResult<GetWordResDTO>> GetAnswer()
    {
        try
        {
            return Ok(await _service.GetAnswerFromDictionary("five-letter-words.json"));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("check")]
    public async Task<ActionResult<CheckWordResDTO>> CheckGuess(string guess)
    {
        try
        {
            return Ok(new CheckWordResDTO()
            {
                Response = await _service.CheckWordExists("five-letter-words.json", guess)
            });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("fetchAllWords")]
    public async Task<ActionResult> FetchAllWords()
    {
        try
        {
            var writerService = new FileReaderService();
            var result = await writerService.FetchAllWords();
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}