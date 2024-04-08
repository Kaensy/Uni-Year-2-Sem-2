namespace MPP_CSharp.Domain
{
    public class TrackDTO
    {
        public Track Track { get; set; }
        public string TrackName { get; set; }
        public int NrParticipants { get; set; }

        public TrackDTO(Track track, int nrParticipants)
        {
            Track = track;
            TrackName = track.Name;
            NrParticipants = nrParticipants;
        }
    }
}