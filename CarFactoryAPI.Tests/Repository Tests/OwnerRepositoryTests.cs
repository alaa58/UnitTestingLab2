using CarAPI.Entities;
using CarFactoryAPI.Entities;
using CarFactoryAPI.Repositories_DAL;
using Moq;
using Moq.EntityFrameworkCore;

public class OwnerRepositoryTests
{
    Mock<FactoryContext> mockfactor;
    OwnerRepository ownerRepository;

    public OwnerRepositoryTests()
    {
        mockfactor = new Mock<FactoryContext>();

        ownerRepository = new OwnerRepository(mockfactor.Object);

    }



    [Fact]
    public void GetOwnerById_SearchID2_FindOwner()
    {
        List<Owner> owners = new List<Owner>()
            {
                new Owner (){Id = 1 , Name = "Ahmed"},
                new Owner (){Id = 2 , Name = "Aya"},
                new Owner (){Id = 3 , Name = "Sara"}

            };

        mockfactor.Setup(e => e.Owners).ReturnsDbSet(owners);
        Owner own = ownerRepository.GetOwnerById(3);

        Assert.Equal(3, own.Id);
    }
}
