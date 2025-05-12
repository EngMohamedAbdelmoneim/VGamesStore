using VGameStore.Core.Entities;
using VGameStore.Core.Specifications;
public class GameWithGenresAndImagesSpec : BaseSpecification<Game>
{
	public GameWithGenresAndImagesSpec()
	{
		AddInclude(g => g.GameGenres);             
		AddInclude("GameGenres.Genre");            
		AddInclude(g => g.Images);                  
	}

	public GameWithGenresAndImagesSpec(int id)
		: base(g => g.Id == id)
	{
		AddInclude(g => g.GameGenres);
		AddInclude("GameGenres.Genre");
		AddInclude(g => g.Images);
	}
}
