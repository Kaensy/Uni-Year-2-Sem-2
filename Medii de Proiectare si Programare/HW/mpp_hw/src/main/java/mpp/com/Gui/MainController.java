package mpp.com.Gui;

import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Stage;
import mpp.com.Domain.Child;
import mpp.com.Domain.Track;
import mpp.com.Domain.User;
import mpp.com.Service.Service;

import java.io.IOException;
import java.util.List;
import java.util.Map;
import java.util.Set;
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
    public TableView<Track> tableViewTracks;
    public TableColumn<Track,String> tableColumnTracksName;
    public Button logoutButton;

    private Service masterService;
    private Stage dialogStage;
    private User currentUser;
    private final ObservableList<Child> modelChild = FXCollections.observableArrayList();
    private final ObservableList<Track> modelTrack = FXCollections.observableArrayList();
    private final ObservableList<Track> modelTrackAgeIntervals = FXCollections.observableArrayList();

    public void setMasterService(Service service, Stage stage, User user) {
        this.masterService = service;
        this.dialogStage = stage;
        this.currentUser = user;
        initModelChild(masterService.getAllChildren());
        initModelTrack(masterService.getAllTracks());
        initModelTrackAgeInterval(masterService.getAllTracks());
    }

    private void initModelChild(Iterable<Child> children) {
        List<Child> childList = StreamSupport.stream(children.spliterator(), false).toList();
        modelChild.setAll(childList);
    }

    private void initModelTrack(Iterable<Track> tracks) {
        List<Track> trackList = StreamSupport.stream(tracks.spliterator(), false).toList();
        modelTrack.setAll(trackList);
    }

    private void initModelTrackAgeInterval(Iterable<Track> tracks) {
        Map<String, Track> uniqueAgeIntervalTracksMap = StreamSupport.stream(tracks.spliterator(), false)
                .collect(Collectors.toMap(Track::getAgeInterval, track -> track, (existing, replacement) -> existing));

        modelTrackAgeIntervals.setAll(uniqueAgeIntervalTracksMap.values());
    }

    @FXML
    public void initialize(){
        tableColumnChildName.setCellValueFactory(new PropertyValueFactory<>("name"));
        tableColumnChildAge.setCellValueFactory(new PropertyValueFactory<>("age"));
        tableColumnChildTracks.setCellValueFactory(new PropertyValueFactory<>("numberOfTracks"));
        tableViewChild.setItems(modelChild);

        tableColumnTracksName.setCellValueFactory(new PropertyValueFactory<>("name"));
        tableViewTracks.setItems(modelTrack);

        tableColumnAgeGroupsIntervals.setCellValueFactory(new PropertyValueFactory<>("ageInterval"));
        tableViewAgeGroups.setItems(modelTrackAgeIntervals);
    }


    public void handleAdd(ActionEvent actionEvent) {
        var name = nameTextField.getText();
        var age = Integer.parseInt(ageTextField.getText());

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
            masterService.addChild(name, age, trackValues);
        } catch (Exception e) {
            Alert alert = new Alert(Alert.AlertType.ERROR);
            alert.setTitle("Error Dialog");
            alert.setHeaderText("Input Error");
            alert.setContentText(e.getMessage());

            alert.showAndWait();
            return;
        }
        initModelChild(masterService.getAllChildren());
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
}
