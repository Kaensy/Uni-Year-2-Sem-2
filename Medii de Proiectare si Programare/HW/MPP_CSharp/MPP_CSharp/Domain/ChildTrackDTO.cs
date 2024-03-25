namespace MPP_CSharp.Domain;

public class ChildTrackDTO
{
    public Child Child { get; set; }
    public int TrackCount { get; set; }

    public ChildTrackDTO(Child child, int trackCount)
    {
        this.Child = child;
        this.TrackCount = trackCount;
    }
    
}