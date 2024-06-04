module iss.frontend {
    requires javafx.controls;
    requires javafx.fxml;

    requires org.controlsfx.controls;
    requires com.dlsc.formsfx;

    opens iss.frontend to javafx.fxml;
    exports iss.frontend;
}