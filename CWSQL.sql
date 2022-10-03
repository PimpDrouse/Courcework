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
 RoleId integer,
 FOREIGN KEY (RoleId) REFERENCES Roles (Id) ON DELETE SET NULL
 );

 INSERT INTO Logins(Login, Password, RoleId) VALUES ('Admin', 'Admin', 1);
 INSERT INTO Logins(Login, Password, RoleId) VALUES ('Doctor', 'Doctor', 2);
 INSERT INTO Logins(Login, Password, RoleId) VALUES ('Patient', 'Patient', 3);
 INSERT INTO Logins(login, password, roleid) VALUES ('doc', 'doc', 2);
 INSERT INTO Logins(login, password, roleid) VALUES ('pat', 'pat', 3);


 --PersonData

 CREATE TABLE PersonData
 (
 Id serial NOT NULL PRIMARY KEY,
 LastName character(50) NOT NULL,
 FirstName character(50) NOT NULL,
 Surname character(50) NULL,
 LoginId integer UNIQUE NOT NULL,
 HospitalId integer,
 FOREIGN KEY (LoginId) REFERENCES Logins (Id) ON DELETE CASCADE,
 FOREIGN KEY (HospitalId) REFERENCES Hospitals (Id) ON DELETE SET NULL
 );

 --Hospitals

 CREATE TABLE Hospitals
 (
 Id serial NOT NULL PRIMARY KEY,
 Name character(100) UNIQUE NOT NULL,
 Location character(200) UNIQUE NOT NULL
 );

 INSERT INTO Hospitals(Name, Location) VALUES ("ФГБУ «Поликлиника № 1» Управления делами Президента Российской Федерации", "пер. Сивцев Вражек, 26/3");
 INSERT INTO Hospitals(Name, Location) VALUES ("Городская поликлиника № 62, филиал № 4", "Ленинградский просп., 18А");
 INSERT INTO Hospitals(Name, Location) VALUES ("Городская поликлиника № 68 ДЗМ", "ул. Малая Якиманка, 22, стр. 1");
 INSERT INTO Hospitals(Name, Location) VALUES ("ГБУЗ городская поликлиника № 3 ДЗМ", "Ермолаевский пер., 22-26с1");

 --Doctors

 CREATE TABLE Doctors
 (
 Id serial NOT NULL PRIMARY KEY,
 Specialisation character(50) NOT NULL,
 Position character(50) NOT NULL,
 Cabinet character(6) UNIQUE NULL,
 PersonDataId integer UNIQUE NOT NULL,
 FOREIGN KEY (PersonDataId) REFERENCES PersonData (Id) ON DELETE CASCADE
 );

 --Patients

 CREATE TABLE Patients
 (
 Id serial NOT NULL PRIMARY KEY,
 MedicalPolicy character(16) UNIQUE NOT NULL,
 PersonDataId integer UNIQUE NOT NULL,
 FOREIGN KEY (PersonDataId) REFERENCES PersonData (Id) ON DELETE CASCADE
 );


 --Appointments

 CREATE TABLE Appointments
 (
 Id serial NOT NULL PRIMARY KEY,
 DateAppointment date NOT NULL,
 TimeAppointment time NOT NULL,
 DoctorId integer NOT NULL,
 PatientId integer,
 FOREIGN KEY (DoctorId) REFERENCES Doctors (Id) ON DELETE CASCADE,
 FOREIGN KEY (PatientId) REFERENCES Patients (Id) ON DELETE SET NULL
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
 GRANT SELECT,UPDATE ON TABLE appointments to doctor;
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

 -- Procedures
	-- For Hospitals

	 CREATE OR REPLACE PROCEDURE insertHospital(name character varying(100), location character varying(200))	--insert
	 LANGUAGE SQL
	 BEGIN ATOMIC
 		INSERT INTO hospitals(name, location) VALUES (name, location) ON CONFLICT DO NOTHING;
	 END;

	CREATE OR REPLACE PROCEDURE deleteHospital(id int)		--delete
	 LANGUAGE SQL
	 BEGIN ATOMIC
 		DELETE FROM hospitals WHERE id = deleteHospital.id;
	 END;

	 CREATE OR REPLACE PROCEDURE updateHospital(id int, name character varying(100), location character varying(200))		--update
	 LANGUAGE SQL
	 BEGIN ATOMIC
 		UPDATE hospitals SET name = updateHospital.name, location=updateHospital.location WHERE id=updateHospital.id;
	 END;

	-- For Roles

	CREATE OR REPLACE PROCEDURE insertRole(namee varchar(30))
	AS $$
	BEGIN
		INSERT INTO roles(role) VALUES(namee) ON CONFLICT DO NOTHING;
	END
	$$ LANGUAGE 'plpgsql';


	CREATE OR REPLACE PROCEDURE deleteRole(namee varchar(30))
	LANGUAGE 'plpgsql'
	AS $$
	BEGIN
		DELETE FROM Roles WHERE role = namee;
		IF (namee = 'Admin' or namee = 'Patient' or namee = 'Doctor' or namee not in (SELECT role FROM Roles)) THEN 
			ROLLBACK;
		ELSE
			COMMIT;
		END IF;
	END;
	$$;

	CREATE OR REPLACE PROCEDURE updateRole(idd integer,namee varchar(30))
	AS $$
	BEGIN
		UPDATE roles SET role = namee WHERE id = idd;
	END
	$$ LANGUAGE 'plpgsql';

	--For appointents
	CREATE OR REPLACE PROCEDURE generateAppointment()	--cursor
	AS $$
		DECLARE
		curs cursor FOR SELECT id FROM doctors;
		docId int;
		timee time;
		BEGIN
			OPEN curs;
			LOOP
				FETCH curs INTO docID;
				IF NOT FOUND THEN EXIT; END IF;
				IF docId not in (SELECT doctorId FROM appointments) THEN
					timee = time '10:00:00';
					WHILE timee != time '20:00:00'
					LOOP
						timee = timee + 30 * interval '1 minute';
						INSERT INTO appointments(dateappointment, timeappointment, doctorid, patientid) VALUES (current_date, timee, docId, NULL);
					END LOOP;
				END IF;
			END LOOP;
		END;
	$$ LANGUAGE 'plpgsql';


	SELECT
		(SELECT
			(SELECT concat_ws(' ', concat_ws(' ', firstname, lastname), CASE WHEN surname is null THEN '' ELSE surname END)
			FROM persondata as per
			WHERE per.id = doc.persondataid) as doctorsname FROM doctors as doc WHERE doc.id = app.doctorid),
		(SELECT doc.cabinet as cabinet FROM doctors as doc WHERE doc.id = app.doctorid),
		(SELECT doc.specialisation as specialisation FROM doctors as doc WHERE doc.id = app.doctorid),
		timeappointment as "time",
		dateappointment as "date",
		CASE WHEN app.patientid is null THEN 'free'
		ELSE (
			SELECT(
				SELECT concat_ws(' ', concat_ws(' ', firstname, lastname), CASE WHEN surname is null THEN '' ELSE surname END)
				FROM (SELECT * FROM persondata) as per
				WHERE per.id = ALL(SELECT persondataid FROM persondata)-- pat.persondataid
			   ) as patientname FROM patients as pat WHERE pat.id = app.patientid)
		END
	FROM appointments as app;

-- View

 SELECT l.id,
    r.role,
    l.login,
    l.password
   FROM logins l
     JOIN roles r ON l.roleid = r.id;

CREATE OR REPLACE FUNCTION updateLoginData() RETURNS TRIGGER 
AS $$
	BEGIN
		IF TG_OP = 'DELETE' THEN
			DELETE FROM logins WHERE login = old.login;
			RETURN OLD;
		ELSIF TG_OP = 'UPDATE' THEN
			IF NEW.role in (SELECT role FROM roles) THEN
				UPDATE logins SET login = new.login, password = new.password, roleid = (SELECT id FROM roles WHERE new.role = role) WHERE login = new.login;
			ELSE
				UPDATE logins SET login = new.login, password = new.password WHERE login = new.login;
			END IF;
			
			RETURN NEW;
		ELSIF TG_OP = 'INSERT' THEN
			IF NEW.role in (SELECT role FROM roles) THEN
				INSERT INTO logins(login, password, roleid) VALUES(new.login, new.password, (SELECT id FROM roles WHERE new.role = role));
			ELSE
				INSERT INTO logins(login, password, roleid) VALUES(new.login, new.password, 3);
			END IF;
		END IF;
	END
$$ LANGUAGE 'plpgsql';

CREATE OR REPLACE TRIGGER updateLoginData
INSTEAD OF UPDATE OR DELETE OR INSERT ON logindata
FOR EACH ROW EXECUTE FUNCTION updateLoginData();

CREATE OR REPLACE PROCEDURE updateLoginData(idd integer,rolee varchar(30), loginn varchar(50), passwordd varchar(50))
LANGUAGE SQL
AS $$
	UPDATE logindata SET login = loginn, password = passwordd, role = rolee WHERE id = idd;
$$;

CREATE OR REPLACE PROCEDURE deleteLoginData(loginn varchar(50))
LANGUAGE SQL
AS $$
	DELETE FROM logindata WHERE login = loginn;
$$;


--Trigger
CREATE OR REPLACE TRIGGER updateRole 
	AFTER UPDATE ON roles
	FOR EACH ROW
	EXECUTE FUNCTION updateRole();


CREATE OR REPLACE FUNCTION updateRole() RETURNS TRIGGER
AS $$
	BEGIN
		IF OLD.role in ('Doctor','Patient','Admin') AND OLD.role <> NEW.role
		THEN
			UPDATE roles SET role = old.role WHERE id = old.id;
			RETURN OLD;
		ELSE
			RETURN NEW;
		END IF;
	END;
$$ LANGUAGE 'plpgsql';


-- Vector Func
CREATE FUNCTION countFreeAppointment() RETURNS TABLE(lname varchar(50), fname varchar(50), surname varchar(50), spec varchar(50), cab varchar(6), cou integer) AS $$
    SELECT pd.lastname, pd.firstname, pd.surname, doc.specialisation, doc.cabinet, count(*) FROM appointments as app
		JOIN doctors as doc ON app.doctorid = doc.id 
		JOIN persondata as pd ON pd.id = doc.persondataid 
		WHERE app.patientid is NULL
		GROUP BY pd.lastname, pd.firstname, pd.surname, doc.specialisation, doc.cabinet
		HAVING doc.cabinet in (SELECT cabinet FROM Doctors)
$$ LANGUAGE SQL;

CREATE OR REPLACE FUNCTION insertPersonData
	(lname varchar(50), fname varchar(50), sname varchar(50), 
	 loginId integer, hospitalId integer)
	RETURNS integer
AS $$
        INSERT INTO persondata(lastname, firstname, surname, loginid, hospitalId) 
		VALUES(lname, fname, sname, loginId, hospitalId) RETURNING id;
$$ LANGUAGE sql;

CREATE OR REPLACE FUNCTION insertDoctor
	(spec varchar(50), pos varchar(50), cab varchar(50), persondataid int) RETURNS integer
AS $$
        INSERT INTO doctors(specialisation, position, cabinet, persondataid) 
		VALUES(spec, pos, cab, persondataid) RETURNING id;
$$ LANGUAGE sql;

CREATE OR REPLACE FUNCTION insertPatient
	(medpol varchar(16), persondataid int) RETURNS integer
AS $$
        INSERT INTO patients(medicalpolicy, persondataid) 
		VALUES(medpol, persondataid) ON CONFLICT DO NOTHING RETURNING id;
$$ LANGUAGE sql;

CREATE OR REPLACE FUNCTION getAppointmentDoc(docId int)
RETURNS TABLE(appid int, patientname varchar(150), cabinet varchar(6), medpol varchar(16), timee time, datee date)
AS $$
	SELECT app.id, concat_ws(' '::text, concat_ws(' '::text, per.lastname, per.firstname), per.surname), doc.cabinet, pat.medicalpolicy, app.timeappointment, app.dateappointment FROM doctors as doc 
	JOIN appointments as app ON doc.id = app.doctorid 
	JOIN patients as pat ON app.patientid=pat.id 
	JOIN persondata as per ON per.id = pat.persondataid
	WHERE doc.id = docid;
$$ LANGUAGE SQL;

CREATE OR REPLACE FUNCTION getDoctorData(loginn varchar(50))
RETURNS TABLE(id int, spec varchar(50), pos varchar(50), cab varchar(6), personDataId int, 
			  lname varchar(50), fname varchar(50), sname varchar(50), loginid int, hospitalid int)
AS $$
	SELECT doc.id, doc.specialisation, CASE WHEN doc.position is not NULL THEN doc.position ELSE '-' END, doc.cabinet, per.id, per.lastname, per.firstname, CASE WHEN per.surname is not NULL THEN per.surname ELSE '-' END, logg.id, per.hospitalid FROM doctors as doc
	JOIN persondata as per ON doc.persondataid = per.id
	JOIN logins as logg ON per.loginid = logg.id
	WHERE logg.login = loginn;
$$ LANGUAGE SQL;

CREATE OR REPLACE PROCEDURE updatePerson(idd int, lname varchar(50), fname varchar(50), sname varchar(50), hospitalidd int)
AS $$
	UPDATE personData SET lastname = lname, firstname = fname, surname = sname, hospitalid = hospitalidd WHERE id = idd
$$ LANGUAGE SQL;

CREATE OR REPLACE PROCEDURE updateDoctor(idd int, spec varchar(50), pos varchar(50), cab varchar(6))
AS $$
	UPDATE Doctors SET specialisation = spec, position = pos, cabinet = cab WHERE id = idd
$$ LANGUAGE SQL;

CREATE OR REPLACE FUNCTION getPatientData(loginn varchar(50))
RETURNS TABLE(id int, medpol varchar(16), personDataId int, 
			  lname varchar(50), fname varchar(50), sname varchar(50), loginid int, hospitalid int)
AS $$
	SELECT pat.id, pat.medicalpolicy, per.id, per.lastname, per.firstname, CASE WHEN per.surname is not NULL THEN per.surname ELSE '-' END, logg.id, per.hospitalid FROM patients as pat
	JOIN persondata as per ON pat.persondataid = per.id
	JOIN logins as logg ON per.loginid = logg.id
	WHERE logg.login = loginn;
$$ LANGUAGE SQL;

CREATE OR REPLACE FUNCTION getAppointmentPat(patId int)
RETURNS TABLE(appid int, doctorname varchar(150), spec varchar(50), pos varchar(50), cabinet varchar(6), timee time, datee date)
AS $$
	SELECT app.id, concat_ws(' '::text, concat_ws(' '::text, per.lastname, per.firstname), per.surname), doc.specialisation, CASE WHEN doc.position is not NULL THEN doc.position ELSE '-' END ,doc.cabinet, app.timeappointment, app.dateappointment FROM patients as pat
	JOIN appointments as app ON pat.id = app.patientid 
	JOIN doctors as doc ON app.doctorid=doc.id 
	JOIN persondata as per ON per.id = doc.persondataid
	WHERE pat.id = patId;
$$ LANGUAGE SQL;

CREATE OR REPLACE FUNCTION getFreeAppointmentOnSpec(spec varchar(50))
RETURNS TABLE(appid int, doctorname varchar(150), pos varchar(50), cabinet varchar(6), timee time, datee date)
AS $$
	SELECT app.id, concat_ws(' '::text, concat_ws(' '::text, per.lastname, per.firstname), per.surname), CASE WHEN doc.position is not NULL THEN doc.position ELSE '-' END ,doc.cabinet, app.timeappointment, app.dateappointment FROM appointments as app
	JOIN doctors as doc ON doc.id = app.doctorid 
	JOIN persondata as per ON per.id = doc.persondataid
	WHERE doc.specialisation = spec and app.patientid is NULL;
$$ LANGUAGE SQL;

--Indexes
CREATE INDEX loginRoleId ON logins(roleid);

CREATE INDEX appointmentsIdx ON appointments(timeappointment);

CREATE INDEX appointmentsIdx ON appointments(dateappointment);