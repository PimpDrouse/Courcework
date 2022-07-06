--Roles

 CREATE TABLE Roles
 (Id serial NOT NULL PRIMARY KEY, Role character(30) UNIQUE NOT NULL);
 INSERT INTO Roles (Role) VALUES ('Admin');
 INSERT INTO Roles (Role) VALUES ('Doctor');
 INSERT INTO Roles (Role) VALUES ('Patient');


 --Logins

CREATE TABLE Logins
 (
 Id serial NOT NULL PRIMARY KEY,
 Login character(50) UNIQUE NOT NULL,
 Password character(50) NOT NULL,
 FirstEnter boolean NOT NULL DEFAULT true,
 RoleId integer NOT NULL,
 FOREIGN KEY (RoleId) REFERENCES Roles (Id)
 );

 INSERT INTO Logins(Login, Password, RoleId) VALUES ('Login', 'Password', 1);
 INSERT INTO Logins(Login, Password, RoleId) VALUES ('Doctor', 'Doctor', 2);
 INSERT INTO Logins(Login, Password, RoleId) VALUES ('Patient', 'Patient', 3);


 --PersonData

 CREATE TABLE PersonData
 (
 Id serial NOT NULL PRIMARY KEY,
 LastName character(50) NOT NULL,
 FirstName character(50) NOT NULL,
 Surname character(50) NULL,
 LoginId integer NOT NULL,
 HospitalId integer NOT NULL,
 FOREIGN KEY (LoginId) REFERENCES Logins (Id),
 FOREIGN KEY (HospitalId) REFERENCES Hospitals (Id)
 );

 --Hospitals

 CREATE TABLE Hospitals
 (
 Id serial NOT NULL PRIMARY KEY,
 Name character(100) UNIQUE NOT NULL,
 Location character(200) UNIQUE NOT NULL
 );

 --Doctors

 CREATE TABLE Doctors
 (
 Id serial NOT NULL PRIMARY KEY,
 Specialisation character(50) NOT NULL,
 Position character(50) NOT NULL,
 Cabinet character(6) UNIQUE NULL,
 PersonDataId integer NOT NULL,
 FOREIGN KEY (PersonDataId) REFERENCES PersonData (Id)
 );

 --Patients

 CREATE TABLE Patients
 (
 Id serial NOT NULL PRIMARY KEY,
 MedicalPolicy character(16) UNIQUE NOT NULL,
 PersonDataId integer NOT NULL,
 FOREIGN KEY (PersonDataId) REFERENCES PersonData (Id)
 );


 --Appointments

 CREATE TABLE Appointments
 (
 Id serial NOT NULL PRIMARY KEY,
 DateAppointment date NOT NULL,
 TimeAppointment time NOT NULL,
 DoctorId integer NOT NULL,
 PatientId integer NOT NULL,
 FOREIGN KEY (DoctorId) REFERENCES Doctors (Id),
 FOREIGN KEY (PatientId) REFERENCES Patients (Id)
 );

 --Create Roles
 CREATE ROLE admin WITH LOGIN PASSWORD 'admin';
 CREATE ROLE patient WITH LOGIN PASSWORD 'patient';
 CREATE ROLE doctor WITH LOGIN PASSWORD 'doctor';

 GRANT ALL PRIVILEGES on DATABASE courcework to admin;
 GRANT ALL PRIVILEGES on table doctors to admin;
 GRANT ALL PRIVILEGES on table appointments to admin;
 GRANT ALL PRIVILEGES on table patients to admin;
 GRANT ALL PRIVILEGES on table hospitals to admin;
 GRANT ALL PRIVILEGES on table logins to admin;
 GRANT ALL PRIVILEGES on table persondata to admin;
 GRANT ALL PRIVILEGES on table roles to admin;

 GRANT SELECT,UPDATE,DELETE ON TABLE doctors to doctor;
 GRANT SELECT,UPDATE,DELETE ON TABLE persondata to doctor;
 GRANT SELECT,UPDATE,DELETE ON TABLE logins to doctor;
 GRANT SELECT ON TABLE appointments to doctor;
 GRANT SELECT ON TABLE hospitals to doctor;
 GRANT SELECT ON TABLE roles to doctor;

 GRANT SELECT,UPDATE,DELETE ON TABLE patients to patient;
 GRANT SELECT,UPDATE,DELETE ON TABLE persondata to patient;
 GRANT SELECT,UPDATE,DELETE ON TABLE logins to patient;
 GRANT SELECT,UPDATE ON TABLE appointments to patient;
 GRANT SELECT ON TABLE hospitals to patient;
 GRANT SELECT ON TABLE roles to patient;

 -- для INSERT serial полей
 GRANT ALL PRIViLEGES ON TABLE appointments_id_seq TO admin;
 GRANT ALL PRIViLEGES ON TABLE doctors_id_seq TO admin;
 GRANT ALL PRIViLEGES ON TABLE patients_id_seq TO admin;
 GRANT ALL PRIViLEGES ON TABLE hospitals_id_seq TO admin;
 GRANT ALL PRIViLEGES ON TABLE logins_id_seq TO admin;
 GRANT ALL PRIViLEGES ON TABLE roles_id_seq TO admin;
 GRANT ALL PRIViLEGES ON TABLE persondata_id_seq TO admin;