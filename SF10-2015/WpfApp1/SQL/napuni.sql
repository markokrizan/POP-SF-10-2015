INSERT INTO TipNamestaja  VALUES ('Krevet', 0);
INSERT INTO TipNamestaja  VALUES ('Ugaona garnitura', 0);
INSERT INTO TipNamestaja  VALUES ('Sofa', 0);

INSERT INTO Akcije
    VALUES( 0, 'Novogosinja', 10, '01-12-2017', '12-01-2018');

--ovde cemo jos videti da li ne treba ID
/*
INSERT INTO Namestaj (Naziv, Sifra, tipNamestajaID, Cena, Kolicina, Obrisan, AkcijaID)
	VALUES ('Francuski krevet', 'a101', 1, 15000, 22, 0, 1);
INSERT INTO Namestaj (Naziv, Sifra, tipNamestajaID, Cena, Kolicina, Obrisan, AkcijaID)
	VALUES ('Sofija ugaona', 'a102', 1,  16700, 12, 0, 1);
INSERT INTO Namestaj (Naziv, Sifra, tipNamestajaID, Cena, Kolicina, Obrisan, AkcijaID)
	VALUES ('Ivan kauc', 'a103', 2,  13000, 2, 0, 1);
INSERT INTO Namestaj (Naziv, Sifra, tipNamestajaID, Cena, Kolicina, Obrisan, AkcijaID)
    VALUES ('Ivan kauc', 'a104', 3, 12000, 2, 0, 1);
*/

INSERT INTO Namestaj (Naziv, Sifra, tipNamestajaID, Cena, Kolicina, Obrisan)
    VALUES ('Francuski krevet', 'a101', 1, 15000, 22, 0);
INSERT INTO Namestaj (Naziv, Sifra, tipNamestajaID, Cena, Kolicina, Obrisan)
    VALUES ('Sofija ugaona', 'a102', 1,  16700, 12, 0);
INSERT INTO Namestaj (Naziv, Sifra, tipNamestajaID, Cena, Kolicina, Obrisan)
    VALUES ('Neki kauc', 'a103', 2,  13000, 2, 0);
INSERT INTO Namestaj (Naziv, Sifra, tipNamestajaID, Cena, Kolicina, Obrisan)
    VALUES ('Radnom kauc', 'a104', 3, 12000, 2, 0);




INSERT INTO DodatneUsluge
    VALUES(0, 'Dostava', 2000);
INSERT INTO DodatneUsluge
    VALUES(0, 'Sastavljanje', 3000);


INSERT INTO Korisnici
    VALUES(0, 'Petar', 'Petrovic', 'pekip', 'p123', 1);
INSERT INTO Korisnici
    VALUES(0, 'Sava', 'Savanovic', 'savas', 's456', 2);



INSERT INTO Salon
    VALUES('Jugodrvo', 'Puskinova 1', '021-222-333', 'jugodrvo@salon.com', '9394969493', '9399993333', '160-2330000200-300', 'www.jugodrvosalon.com');



INSERT INTO Racun
    VALUES('02-12-2017', '101', 'Jovica', 17000 );



INSERT INTO ProdatiNamestaj
    VALUES(1, 1);


INSERT INTO ProdateUsluge
    VALUES(1, 1);













	




/* 
	VRACA CONNECTION STRING
	select
    'data source=' + @@servername +
    ';initial catalog=' + db_name() +
    case type_desc
        when 'WINDOWS_LOGIN' 
            then ';trusted_connection=true'
        else
            ';user id=' + suser_name()
    end
from sys.server_principals
where name = suser_name()
*/


