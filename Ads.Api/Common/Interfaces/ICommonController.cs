using Microsoft.AspNetCore.Mvc;

namespace Ads.Api.Common.Interfaces
{
    public interface ICommonController<TCreateCommand, TUpdateCommand>
    {
        Task<IActionResult> GetAll(CancellationToken cancellationToken);
        Task<IActionResult> GetById(string id, CancellationToken cancellationToken);
        Task<IActionResult> Create([FromBody] TCreateCommand command, CancellationToken cancellationToken);
        Task<IActionResult> Update(TUpdateCommand command, CancellationToken cancellationToken);
        Task<IActionResult> Delete(string id, CancellationToken cancellationToken);
    }
}
