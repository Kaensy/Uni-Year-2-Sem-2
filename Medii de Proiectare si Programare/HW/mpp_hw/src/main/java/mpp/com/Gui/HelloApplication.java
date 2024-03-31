package mpp.com.Gui;

import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Stage;

import java.io.IOException;

public class HelloApplication extends Application{

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

    }

    public static void main(String[] args) {
        launch();
    }


}
