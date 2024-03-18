using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Lab1_SGBD.repository;

namespace Lab1_SGBD.service
{
    public class Service
    {
        private readonly Repository repository;
        public Service(Repository repository)
        {
            this.repository = repository;
        }

        public DataTable GetAllOrganizers()
        {
            return repository.GetAllOrganizers();
        }

        public DataTable GetTournamentsByOrganizerID(int parentId) {
            return repository.GetTournamentsByOrganizerID(parentId);
        }

        public void AddTournament(String TournamentName, DateTime StartDate, DateTime EndDate, String TournamentLocation, float PrizePool, int GameID, int OrganizerID)
        {
            repository.AddTournament(TournamentName, StartDate, EndDate, TournamentLocation, PrizePool, GameID, OrganizerID);
        }

        public void UpdateTournament(String TournamentName, DateTime StartDate, DateTime EndDate, String TournamentLocation, float PrizePool, int GameID, int OrganizerID)
        {
            repository.UpdateTournament(TournamentName, StartDate, EndDate, TournamentLocation, PrizePool, GameID, OrganizerID);
        }

        public void DeleteTournament(String TournamentName)
        {
            repository.DeleteTournament(TournamentName);
        }
    }
}
