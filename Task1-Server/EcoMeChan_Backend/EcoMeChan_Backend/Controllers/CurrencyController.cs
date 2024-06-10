// CurrencyController.cs
using Microsoft.AspNetCore.Mvc;
using EcoMeChan.Services.Interfaces;
using EcoMeChan.ViewModels;
using EcoMeChan.ViewModels.Create;
using EcoMeChan.ViewModels.Edit;

[ApiController]
[Route("api/[controller]")]
public class CurrencyController : ControllerBase
{
    private readonly ICurrencyService _currencyService;

    public CurrencyController(ICurrencyService currencyService)
    {
        _currencyService = currencyService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CurrencyViewModel>>> GetAll()
    {
        var currencies = await _currencyService.GetAllAsync();
        return Ok(currencies);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CurrencyViewModel>> GetById(int id)
    {
        var currency = await _currencyService.GetByIdAsync(id);
        if (currency == null)
        {
            return NotFound();
        }
        return Ok(currency);
    }

    [HttpPost]
    public async Task<ActionResult<CurrencyViewModel>> Create([FromBody] CurrencyCreateViewModel currencyCreateViewModel)
    {
        var createdCurrency = await _currencyService.CreateAsync(currencyCreateViewModel);
        return CreatedAtAction(nameof(GetById), new { id = createdCurrency.Id }, createdCurrency);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CurrencyViewModel>> Update(int id, [FromBody] CurrencyEditViewModel currencyEditViewModel)
    {
        var updatedCurrency = await _currencyService.UpdateAsync(id, currencyEditViewModel);
        return Ok(updatedCurrency);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _currencyService.DeleteAsync(id);
        return NoContent();
    }
}