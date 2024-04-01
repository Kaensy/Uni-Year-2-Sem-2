package mpp.com.Gui;

import javafx.fxml.FXML;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.control.TextField;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Modality;
import javafx.stage.Stage;
import mpp.com.Service.Service;

public class LoginController {

    @FXML
    Button loginButton;
    @FXML
    TextField usernameField;
    @FXML
    TextField passwordField;

    private Service service;
    private Stage dialogStage;

    public void setMasterService(Service service, Stage dialogStage) {
        this.service = service;
        this.dialogStage = dialogStage;
        usernameField.setText("lau");
        passwordField.setText("lau");
    }

    @FXML
    public void handleLogin(ActionEvent event){
        String username = usernameField.getText();
        String password = passwordField.getText();
        try{
            var oUser = this.service.searchUsedByUsernamePassword(username,password);
            boolean loginSuccesful = oUser.isPresent();
            if(loginSuccesful){

                FXMLLoader loader = new FXMLLoader();
                loader.setLocation(getClass().getResource("/mpp.com/Gui/mainView.fxml"));

                AnchorPane root = loader.load();
                Scene scene = new Scene(root);
                Stage stage = new Stage();
                stage.initModality(Modality.APPLICATION_MODAL);
                stage.setScene(scene);

                MainController mainController = loader.getController();
                mainController.setMasterService(service,stage,oUser.get());
                stage.show();
                dialogStage.close();
            }else{
                FXMLLoader loader = new FXMLLoader();
                loader.setLocation(getClass().getResource("/mpp.com/Gui/loginView.fxml"));

                AnchorPane root = loader.load();
                Scene scene = new Scene(root);
                Stage stage = new Stage();
                stage.initModality(Modality.APPLICATION_MODAL);
                stage.setScene(scene);

                LoginController loginController = loader.getController();
                loginController.setMasterService(service,stage);
                stage.show();
                dialogStage.close();
            }
        }catch (Exception e){
            e.printStackTrace();
        }
    }

}
