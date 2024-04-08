using System;
using System.Linq;
using System.Windows.Forms;
using MPP_CSharp.Domain;

namespace MPP_CSharp;

public partial class MainWindow : Form
{
    private Service.Service _service;
    private User _user;
    private Form1 _form1;
    
    public MainWindow(User user, Service.Service service, Form1 form1)
    {
        InitializeComponent();
        _user = user;
        _service = service;
        _form1 = form1;
        
    }


    private void buttonExit_Click(object sender, EventArgs e)
    {
        _form1.Show();
        Close();
    }


    private void buttonAdd_Click(object sender, EventArgs e)
    {
        var name = NameTextBox.Text;
        var age = Convert.ToInt32(AgeTextBox.Text);
        string trackTextFieldValue = textBox1.Text;
        string[] trackValues = trackTextFieldValue.Split(',');
        _service.AddChild(name, age, trackValues);
        MainWindow_Load(sender, e);
        NameTextBox.Clear();
        AgeTextBox.Clear();
        textBox1.Clear();
    }

    private void MainWindow_Load(object sender, EventArgs e)
    {
        var children = _service.GetAllChildren()
            .Select(child => new
            {
                Id = child.Id,
                Name = child.Name, 
                Age = child.Age,
                nrTracks = child.Tracks.Count()
            }).ToList();
        dataGridViewChildren.DataSource = children;
        dataGridViewChildren.Columns["Id"].Visible = false;
        
        var uniqueAgeIntervalTracksMap = _service.GetAllTracks()
            .GroupBy(track => track.GetAgeInterval())
            .ToDictionary(group => group.Key, group => group.First());

        var sortedAgeIntervals = uniqueAgeIntervalTracksMap.Values
            .OrderBy(track => int.Parse(track.GetAgeInterval().Split('-')[0]))
            .Select(track => new { AgeInterval = track.GetAgeInterval() })
            .ToList();

        dataGridViewAges.DataSource = sortedAgeIntervals;
        
        var trackDTOs = _service.GetTrackDTOs().Select(track => new
        {
            Name = track.Track.Name,
            Track = track.Track,
            nrParticipants = track.NrParticipants
        }).ToList();
        dataGridViewTracks.DataSource = trackDTOs;
        dataGridViewTracks.Columns["Track"].Visible = false;
        
    }
    
    private void LoadTracksByAge(dynamic ageInterval)
    {
        var ageIntervalString = ((object)ageInterval).ToString();
        var ageIntervalParts = ageIntervalString.Split('=')[1].Split('-');
        var minimumAge = Convert.ToInt32(ageIntervalParts[0]);
        var maximumAge = Convert.ToInt32(ageIntervalParts[1].Replace("}", ""));
        var tracks = _service.GetTrackDTOsByAge(minimumAge, maximumAge)
            .Select(t => new
            {
                Name = t.Track.Name,
                Track = t.Track,
                nrParticipants = t.NrParticipants
            }).ToList();
        dataGridViewTracks.DataSource = tracks;
        dataGridViewTracks.Columns["Track"].Visible = false;
    }

    private void dataGridViewAges_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        var selectedAge = dataGridViewAges.CurrentRow?.DataBoundItem as dynamic;

        if (selectedAge == null) return;
        LoadTracksByAge(selectedAge);
    }


    private void dataGridViewTracks_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        var selectedTrackDTO = dataGridViewTracks.CurrentRow?.DataBoundItem as dynamic;
        if (selectedTrackDTO == null) return;

        Track selectedTrack = selectedTrackDTO.Track;
        LoadChildrenByTrack(selectedTrack);
    }

    private void LoadChildrenByTrack(Track selectedTrack)
    {
        
        var children = _service.GetChildrenByTrackId(selectedTrack.Id)
            .Select(child => new
            {
                Id = child.Id,
                Name = child.Name, 
                Age = child.Age,
                nrTracks = child.Tracks.Count()
            }).ToList();
        dataGridViewChildren.DataSource = children;
        dataGridViewChildren.Columns["Id"].Visible = false;
    }
}