package mpp.com.Gui;

import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Stage;
import mpp.com.Repository.*;
import mpp.com.Service.Service;
import mpp.com.Service.ServiceChild;
import mpp.com.Service.ServiceTrack;
import mpp.com.Service.ServiceUser;

import java.io.FileReader;
import java.io.IOException;
import java.util.Properties;

public class HelloApplication extends Application{

    public static Service service;

    @Override
    public void start(Stage stage) throws IOException {
        initView(stage);
        stage.show();
    }

    private void initView(Stage primaryStage) throws IOException{
        FXMLLoader messageLoader = new FXMLLoader();
        messageLoader.setLocation(getClass().getResource("/mpp.com/Gui/loginView.fxml"));
        AnchorPane messageTaskLayout = messageLoader.load();
        primaryStage.setScene(new Scene(messageTaskLayout));

        LoginController loginController = messageLoader.getController();
        loginController.setMasterService(service, primaryStage);
    }

    public static void main(String[] args) {

        Properties props = new Properties();
        try{
            props.load(new FileReader("bd.config"));
        } catch (IOException e) {
            throw new RuntimeException(e);
        }


        RepositoryTrack RepoDBTrack = new RepoDBTrack(props);
        RepositoryUser RepoDBUser = new RepoDBUser(props);
        RepositoryChild RepoDBChild = new RepoDBChild(props);
        service = new Service(new ServiceUser(RepoDBUser), new ServiceTrack(RepoDBTrack), new ServiceChild(RepoDBChild));

        launch();
    }

}
