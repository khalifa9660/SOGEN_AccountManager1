using Xunit;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoGen_AccountManager1.Controllers;
using SoGen_AccountManager1.Models.DTO;
using SoGen_AccountManager1.Repositories.Interface;
using SoGen_AccountManager1.Models.Domain;
using NuGet.ContentModel;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace SoGenXUnitTest;

public class PlayerControllerTests
{
    // Test pour vérifier le bon fonctionnement de la méthode AddPlayer
    [Fact]
    public async Task AddPlayer_Returns_OkResult()
    {
        // Arrange
        var mockRepository = new Mock<IPlayerRepository>();
        var controller = new PlayerController(mockRepository.Object);

        // Simuler un utilisateur authentifié avec un claim d'ID
        var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim("Id", Guid.NewGuid().ToString()),
        }, "mock"));
        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = userClaims }
        };

        var playerToAdd = new UpdatePlayerDTO
        {
            Name = "TestPlayer",
            Age = 25,
            Number = 10,
            Position = "Forward",
            Photo = "photo.jpg"
        };

        // Act
        var result = await controller.AddPlayer(playerToAdd) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<PlayerDto>(result.Value);

        var addedPlayer = result.Value as PlayerDto;
        Assert.Equal(playerToAdd.Name, addedPlayer.Name);
        Assert.Equal(playerToAdd.Position, addedPlayer.Position);
        Assert.Equal(playerToAdd.Number, addedPlayer.Number);
        Assert.Equal(playerToAdd.Photo, addedPlayer.Photo);

        mockRepository.Verify(repo => repo.AddPlayer(It.IsAny<Player>()), Times.Once);
    }


    [Fact]
    public async Task EditPlayer_Returns_OkResult()
    {
        // Arrange
        var mockRepository = new Mock<IPlayerRepository>();
        var controller = new PlayerController(mockRepository.Object);

        var playerToEdit = new UpdatePlayerDTO
        {
            Name = "KhalifaTesst",
            Age = 36,
            Number = 31,
            Position = "Defender",
            Photo = "Khalifa.png"
        };

        // retourner un joueur édité
        var editedPlayer = new Player 
        {
            // Initialisez les propriétés du joueur édité
            Name = playerToEdit.Name,
            Age = playerToEdit.Age,
            Number = playerToEdit.Number,
            Position = playerToEdit.Position,
            Photo = playerToEdit.Photo
        };

        mockRepository.Setup(repo => repo.EditPlayer(It.IsAny<Player>()))
                      .ReturnsAsync(editedPlayer);

        // Act
        var result = await controller.EditPlayer(playerToEdit) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<PlayerDto>(result.Value);

        var returnedPlayer = result.Value as PlayerDto;

        Assert.Equal(playerToEdit.Age, returnedPlayer.Age);
        Assert.Equal(playerToEdit.Number, returnedPlayer.Number);
        Assert.Equal(playerToEdit.Position, returnedPlayer.Position);
        Assert.Equal(playerToEdit.Photo, returnedPlayer.Photo);
    }


    [Fact]
    public async Task DeleteMultiple_ReturnsOk_WhenAllPlayersAreDeleted()
    {
        // Arrange
        var mockRepository = new Mock<IPlayerRepository>();
        var controller = new PlayerController(mockRepository.Object);

        var guid1 = Guid.NewGuid().ToString();
        var guid2 = Guid.NewGuid().ToString();
        var ids = $"{guid1},{guid2}";
        mockRepository.Setup(repo => repo.DeletePlayers(It.IsAny<List<Guid>>()))
                      .ReturnsAsync(true);

        // Act
        var result = await controller.DeleteMultiple(ids);

        // Assert
        var actionResult = Assert.IsType<OkResult>(result);
        Assert.NotNull(actionResult);
    }

    [Fact]
    public async Task DeleteMultiple_ReturnsNotFound_WhenAnyPlayerIsNotFound()
    {
        // Arrange
        var mockRepository = new Mock<IPlayerRepository>();
        var controller = new PlayerController(mockRepository.Object);

        var guid1 = Guid.NewGuid().ToString();
        var guid2 = Guid.NewGuid().ToString();
        var ids = $"{guid1},{guid2}";
        mockRepository.Setup(repo => repo.DeletePlayers(It.IsAny<List<Guid>>()))
                      .ReturnsAsync(false);

        // Act
        var result = await controller.DeleteMultiple(ids);

        // Assert
        var actionResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.NotNull(actionResult);
        Assert.Equal("One or more players not found.", actionResult.Value);
    }


}
