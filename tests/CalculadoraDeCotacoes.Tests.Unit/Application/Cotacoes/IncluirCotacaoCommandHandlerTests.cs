using CalculadoraDeCotacoes.Application.Cotacoes.IncluirCotacao;
using CalculadoraDeCotacoes.Persistence.Context;

namespace CalculadoraDeCotacoes.Tests.Unit.Application.Cotacoes;

// public class IncluirCotacaoCommandHandlerTests
// {
//     private readonly ApplicationDbContext _context;
//     private readonly IncluirCotacaoCommandHandler _handler;
//     
//     /// <summary>
//     /// Tests that a valid sale creation request is handled successfully.
//     /// </summary>
//     [Fact(DisplayName = "Given valid sale data When creating sale Then returns success response")]
//     public async Task Handle_ValidRequest_ReturnsSuccessResponse()
//     {
//         // Given
//         // var productId = Guid.NewGuid();
//         // var command = IncluirCotacaoCommandHandlerTestData.GenerateValidCommand(productId);
//         //
//         // const int idCotacao = 1;
//         // var cotacao = new IncluirCotacaoCommand()
//         // {
//         // };
//         //
//         // var result = new IncluirCotacaoResult()
//         // {
//         //     Id = idCotacao,
//         // };
//         //
//         // _mapper.Map<Sale>(command).Returns(sale);
//         // _mapper.Map<CreateSaleResult>(sale).Returns(result);
//         //
//         // _productRepository.GetManyByIdAsync(Arg.Any<IEnumerable<Guid>>(), Arg.Any<CancellationToken>())
//         //     .Returns(ids);
//         //
//         // _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
//         //     .Returns(sale);
//         //
//         // // When
//         // var createSaleResult = await _handler.Handle(command, CancellationToken.None);
//         //
//         // // Then
//         // createSaleResult.Should().NotBeNull();
//         // createSaleResult.Id.Should().Be(sale.Id);
//         // await _saleRepository.Received(1).CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
//     }
// }