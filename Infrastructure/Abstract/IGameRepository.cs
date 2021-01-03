using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Abstract
{
	public interface IGameRepository
	{
		IEnumerable<Game> Games { get; }
		Game GetGame(int id);
		void SaveGame(Game game);
		void DeleteGame(int gameId);
	}
}
