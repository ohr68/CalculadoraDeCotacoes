using Microsoft.AspNetCore.Mvc;

namespace CalculadoraDeCotacoes.Api.Common;

public class BaseController : ControllerBase
{
    protected IActionResult Ok<T>(T data) =>
        base.Ok(new ApiResponseWithData<T> { Data = data, Success = true });

    protected IActionResult Created<T>(string routeName, object routeValues, T data) =>
        base.CreatedAtRoute(routeName, routeValues, new ApiResponseWithData<T> { Data = data, Success = true });
    
    protected IActionResult OkPaginated<T>(PaginatedList<T> pagedList) =>
        Ok(new PaginatedListResponse<T>()
        {
            Data = pagedList,
            CurrentPage = pagedList.CurrentPage,
            TotalPages = pagedList.TotalPages,
            TotalCount = pagedList.TotalCount,
            Success = true,
        });
}