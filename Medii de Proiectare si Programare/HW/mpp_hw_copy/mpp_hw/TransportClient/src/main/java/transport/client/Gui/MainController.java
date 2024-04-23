package transport.client.Gui;

import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Stage;
import mpp.com.Domain.Child;
import mpp.com.Domain.Track;
import mpp.com.Domain.TrackDTO;
import mpp.com.Domain.User;
import mpp.com.Service.Service;

import java.io.IOException;
import java.util.Comparator;
import java.util.List;
import java.util.Map;
import java.util.stream.Collectors;
import java.util.stream.StreamSupport;

public class MainController {


    public TextField nameTextField;
    public TextField ageTextField;
    public TextField trackTextField;
    public Button addButton;
    public TableView<Child> tableViewChild;
    public TableColumn<Child,String> tableColumnChildName;
    public TableColumn<Child, Integer> tableColumnChildAge;
    public TableColumn<Child,Integer> tableColumnChildTracks;
    public TableView<Track> tableViewAgeGroups;
    public TableColumn<Track,String> tableColumnAgeGroupsIntervals;
    public TableView<TrackDTO> tableViewTracks;
    public TableColumn<TrackDTO,String> tableColumnTracksName;
    public Button logoutButton;
    public TableColumn<TrackDTO,Integer> tableColumnTracksNrParticipants;

    private Service masterService;
    private Stage dialogStage;
    private User currentUser;
    private final ObservableList<Child> modelChild = FXCollections.observableArrayList();
    private final ObservableList<TrackDTO> modelTrack = FXCollections.observableArrayList();
    private final ObservableList<Track> modelTrackAgeIntervals = FXCollections.observableArrayList();

    public void setMasterService(Service service, Stage stage, User user) {
        this.masterService = service;
        this.dialogStage = stage;
        this.currentUser = user;
        initModelChild(masterService.getAllChildren());
        initModelTrack(masterService.getTrackDTOs());
        initModelTrackAgeInterval(masterService.getAllTracks());
    }

    private void initModelChild(Iterable<Child> children) {
        List<Child> childList = StreamSupport.stream(children.spliterator(), false).toList();
        modelChild.setAll(childList);
    }

    private void initModelTrack(Iterable<TrackDTO> tracks) {
        List<TrackDTO> trackList = StreamSupport.stream(tracks.spliterator(), false)
                .sorted(Comparator.comparing(dto -> Integer.parseInt(dto.getTrack().getName().replace("m", ""))))
                .collect(Collectors.toList());
        modelTrack.setAll(trackList);
    }

    private void initModelTrackAgeInterval(Iterable<Track> tracks) {
        Map<String, Track> uniqueAgeIntervalTracksMap = StreamSupport.stream(tracks.spliterator(), false)
                .collect(Collectors.toMap(Track::getAgeInterval, track -> track, (existing, replacement) -> existing));

        List<Track> sortedTracks = uniqueAgeIntervalTracksMap.values().stream()
                .sorted(Comparator.comparing(track -> Integer.parseInt(track.getAgeInterval().split("-")[0])))
                .collect(Collectors.toList());

        modelTrackAgeIntervals.setAll(sortedTracks);
    }

    @FXML
    public void initialize(){
        tableColumnChildName.setCellValueFactory(new PropertyValueFactory<>("name"));
        tableColumnChildAge.setCellValueFactory(new PropertyValueFactory<>("age"));
        tableColumnChildTracks.setCellValueFactory(new PropertyValueFactory<>("numberOfTracks"));
        tableViewChild.setItems(modelChild);

        tableColumnTracksName.setCellValueFactory(new PropertyValueFactory<>("trackName"));
        tableColumnTracksNrParticipants.setCellValueFactory(new PropertyValueFactory<>("nrParticipants"));
        tableViewTracks.setItems(modelTrack);

        tableColumnAgeGroupsIntervals.setCellValueFactory(new PropertyValueFactory<>("ageInterval"));
        tableViewAgeGroups.setItems(modelTrackAgeIntervals);
    }


    public void handleAdd(ActionEvent actionEvent) {
        var name = nameTextField.getText();
        var age = ageTextField.getText();

        if ( name.isEmpty() || age.isEmpty()){
            Alert alert = new Alert(Alert.AlertType.ERROR);
            alert.setTitle("Error Dialog");
            alert.setHeaderText("Input Error");
            alert.setContentText("Please enter a name and an age.");

            alert.showAndWait();
            return;
        }
        int age1 =  Integer.parseInt(age);
        String trackTextFieldValue = trackTextField.getText();
        String[] trackValues = trackTextFieldValue.split(",");

        if (trackValues.length < 1 || trackValues.length > 2) {
            Alert alert = new Alert(Alert.AlertType.ERROR);
            alert.setTitle("Error Dialog");
            alert.setHeaderText("Input Error");
            alert.setContentText("Please enter one or two track values separated by a comma.");

            alert.showAndWait();
            return;
        }

        try{
            masterService.addChild(name, age1, trackValues);
        } catch (Exception e) {
            Alert alert = new Alert(Alert.AlertType.ERROR);
            alert.setTitle("Error Dialog");
            alert.setHeaderText("Input Error");
            alert.setContentText(e.getMessage());

            alert.showAndWait();
            return;
        }
        initModelChild(masterService.getAllChildren());
        initModelTrack(masterService.getTrackDTOs());
    }

    public void handleLogout(ActionEvent actionEvent) {
        try {
            FXMLLoader loader = new FXMLLoader();
            loader.setLocation(getClass().getResource("/mpp.com/Gui/loginView.fxml"));

            AnchorPane root = loader.load();

            dialogStage.setTitle("Log in");
            Scene scene = new Scene(root);

            LoginController loginController = loader.getController();
            loginController.setMasterService(this.masterService, dialogStage);

            dialogStage.setScene(scene);
        } catch (IOException e) {
            throw new RuntimeException(e);
        }
    }

    public void handleTableClick(MouseEvent mouseEvent) {
        var selectedTrack = tableViewAgeGroups.getSelectionModel().getSelectedItem();
        if (selectedTrack == null) {
            return;
        }
        initModelTrack(masterService.getTrackDTOsByAge(selectedTrack.getMinimumAge(), selectedTrack.getMaximumAge()));
        tableViewAgeGroups.getSelectionModel().clearSelection();
    }

    public void handleTableTracksClick(MouseEvent mouseEvent) {
        var selectedTrack = tableViewTracks.getSelectionModel().getSelectedItem();
        if (selectedTrack == null) {
            return;
        }
        initModelChild(masterService.getChildrenByTrackId(selectedTrack.getTrack().getId()));
        tableViewTracks.getSelectionModel().clearSelection();
    }
}
