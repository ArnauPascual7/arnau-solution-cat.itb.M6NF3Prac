DROP TABLE IF EXISTS departments;
DROP TABLE IF EXISTS employees;
DROP TABLE IF EXISTS clients;

CREATE TABLE departments (
 _id  INTEGER NOT NULL PRIMARY KEY,
 name  VARCHAR(15), 
 loc   VARCHAR(15)
);

INSERT INTO departments VALUES (10,'CONTABILIDAD','SEVILLA');
INSERT INTO departments VALUES (20,'INVESTIGACIÓN','MADRID');
INSERT INTO departments VALUES (30,'VENTAS','BARCELONA');
INSERT INTO departments VALUES (40,'PRODUCCIÓN','BILBAO');
COMMIT;

CREATE TABLE employees (
 _id    INTEGER  NOT NULL PRIMARY KEY,
 surname  VARCHAR(10),
 job    VARCHAR(10),
 managerid INTEGER,
 startdate DATE      ,
 salary   DECIMAL(6,2),
 commission  DECIMAL(6,2),
 depid   INTEGER NOT NULL,
 CONSTRAINT FK_DEP FOREIGN KEY (depid) REFERENCES departments(_id)

);

INSERT INTO employees VALUES (7369,'SÁNCHEZ','EMPLEADO',7902,'1990/12/17',
                        1040,NULL,20);				
INSERT INTO employees VALUES (7499,'ARROYO','VENDEDOR',7698,'1990/02/20',
                        1500,390,30);
INSERT INTO employees VALUES (7521,'SALA','VENDEDOR',7698,'1991/02/22',
                        1625,650,30);
INSERT INTO employees VALUES (7566,'JIMÉNEZ','DIRECTOR',7839,'1991/04/02',
                        2900,NULL,20);
INSERT INTO employees VALUES (7654,'MARTÍN','VENDEDOR',7698,'1991/09/29',
                        1600,1020,30);
INSERT INTO employees VALUES (7698,'NEGRO','DIRECTOR',7839,'1991/05/01',
                        3005,NULL,30);
INSERT INTO employees VALUES (7782,'CEREZO','DIRECTOR',7839,'1991/06/09',
                        2885,NULL,10);
INSERT INTO employees VALUES (7788,'GIL','ANALISTA',7566,'1991/11/09',
                        3000,NULL,20);
INSERT INTO employees VALUES (7839,'REY','PRESIDENTE',NULL,'1991/11/17',
                        4100,NULL,10);
INSERT INTO employees VALUES (7844,'TOVAR','VENDEDOR',7698,'1991/09/08',
                        1350,0,30);
INSERT INTO employees VALUES (7876,'ALONSO','EMPLEADO',7788,'1991/09/23',
                        1430,NULL,20);
INSERT INTO employees VALUES (7900,'JIMENO','EMPLEADO',7698,'1991/12/03',
                        1335,NULL,30);
INSERT INTO employees VALUES (7902,'FERNÁNDEZ','ANALISTA',7566,'1991/12/03',
                        3000,NULL,20);
INSERT INTO employees VALUES (7934,'MUÑOZ','EMPLEADO',7782,'1992/01/23',
                        1690,NULL,10);
COMMIT;


CREATE TABLE IF NOT EXISTS clients (
 _id          INTEGER PRIMARY KEY,
 name                 VARCHAR (45) NOT NULL,
 address               VARCHAR (40) NOT NULL,
 city              VARCHAR (30) NOT NULL,
 st               VARCHAR (2),
 zipcode         VARCHAR (9) NOT NULL,
 area                INTEGER,
 phone             VARCHAR (9),
 empid            INTEGER,
 credit        DECIMAL(9,2) ,
 comments        VARCHAR,
 FOREIGN KEY (empid) REFERENCES employees(_id));

INSERT INTO clients (_id, name, address , city, st, zipcode, area, 
                      phone, empid, credit, comments)
VALUES (100, 'JOCKSPORTS', '345 VIEWRIDGE', 'BELMONT', 'CA', '96711', 415,
        '598-6609', 7844, 5000, 
        'Very friendly people to work with -- sales rep likes to be called Mike.');
INSERT INTO clients (_id, name, address , city, st, zipcode, area, 
                      phone, empid, credit, comments)
VALUES (101, 'TKB SPORT SHOP', '490 BOLI RD.', 'REDWOOD city', 'CA', '94061', 415,
        '368-1223', 7521, 10000, 
        'Rep called 5/8 about change in order - contact shipping.');
INSERT INTO clients (_id, name, address , city, st, zipcode, area, 
                      phone, empid, credit, comments)
VALUES (102, 'VOLLYRITE', '9722 HAMILTON', 'BURLINGAME', 'CA', '95133', 415,
        '644-3341', 7654, 7000, 
        'Company doing heavy promotion beginning 10/89. Prepare for large orders during winter.');
INSERT INTO clients (_id, name, address , city, st, zipcode, area, 
                      phone, empid, credit, comments)
VALUES (103, 'JUST TENNIS', 'HILLVIEW MALL', 'BURLINGAME', 'CA', '97544', 415,
        '677-9312', 7521, 3000, 
        'Contact rep about new line of tennis rackets.');
INSERT INTO clients (_id, name, address , city, st, zipcode, area, 
                      phone, empid, credit, comments)
VALUES (104, 'EVERY MOUNTAIN', '574 SURRY RD.', 'CUPERTINO', 'CA', '93301', 408,
        '996-2323', 7499, 10000, 
        'clients with high market share (23%) due to aggressive advertising.');
INSERT INTO clients (_id, name, address , city, st, zipcode, area, 
                      phone, empid, credit, comments)
VALUES (105, 'K + T SPORTS', '3476 EL PASEO', 'SANTA CLARA', 'CA', '91003', 408,
        '376-9966', 7844, 5000, 
        'Tends to order large amounts of merchandise at once. Accounting is considering raising their credit limit. Usually pays on time.');
INSERT INTO clients (_id, name, address , city, st, zipcode, area, 
                      phone, empid, credit, comments)
VALUES (106, 'SHAPE UP', '908 SEQUOIA', 'PALO ALTO', 'CA', '94301', 415,
        '364-9777', 7521, 6000, 
        'Support intensive. Orders small amounts (< 800) of merchandise at a time.');
INSERT INTO clients (_id, name, address , city, st, zipcode, area, 
                      phone, empid, credit, comments)
VALUES (107, 'WOMEN SPORTS', 'VALCO VILLAGE', 'SUNNYVALE', 'CA', '93301', 408,
        '967-4398', 7499, 10000, 
        'First sporting goods store geared exclusively towards women. Unusual promotion al style and very willing to take chances towards new PRODUCTEs!');
INSERT INTO clients (_id, name, address , city, st, zipcode, area, 
                      phone, empid, credit, comments)
VALUES (108, 'NORTH WOODS HEALTH AND FITNESS SUPPLY CENTER', '98 LONE PINE WAY', 'HIBBING', 'MN', '55649', 612,
        '566-9123', 7844, 8000, '');
INSERT INTO clients (_id, name, address , city, st, zipcode, area, 
                      phone, empid, credit, comments)
VALUES (109, 'SPRINGFIELD NUCLEAR POWER PLANT', '13 PERCEBE STR.', 'SPRINGFIELD', 'NT', '0000', 636,
        '999-6666', 7934, 10000, '');


