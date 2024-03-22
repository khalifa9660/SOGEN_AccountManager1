// Importation des dépendances nécessaires pour les tests unitaires
using Xunit;
using Moq; // Si vous utilisez Moq pour les stubs/mocks
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoGen_AccountManager1.Controllers;
using SoGen_AccountManager1.Models.DTO;
using SoGen_AccountManager1.Repositories.Interface;
using SoGen_AccountManager1.Models.Domain;

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
}
