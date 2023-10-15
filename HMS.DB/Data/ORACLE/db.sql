--------------------------------------------------------
--  File created - Sunday-October-15-2023   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Sequence BUILDING_SEQ
--------------------------------------------------------

   CREATE SEQUENCE  "MOHAMMED"."BUILDING_SEQ"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 21 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence BUILDING_SEQ1
--------------------------------------------------------

   CREATE SEQUENCE  "MOHAMMED"."BUILDING_SEQ1"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 41 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence COLLAGES_SEQ
--------------------------------------------------------

   CREATE SEQUENCE  "MOHAMMED"."COLLAGES_SEQ"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 21 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence PERMISSIONS_SEQ
--------------------------------------------------------

   CREATE SEQUENCE  "MOHAMMED"."PERMISSIONS_SEQ"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence ROLEPERMISSION_SEQ
--------------------------------------------------------

   CREATE SEQUENCE  "MOHAMMED"."ROLEPERMISSION_SEQ"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence SITEUSER_SEQ
--------------------------------------------------------

   CREATE SEQUENCE  "MOHAMMED"."SITEUSER_SEQ"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence SITEUSER_SEQ1
--------------------------------------------------------

   CREATE SEQUENCE  "MOHAMMED"."SITEUSER_SEQ1"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 21 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Table BUILDING
--------------------------------------------------------

  CREATE TABLE "MOHAMMED"."BUILDING" 
   (	"ID" NUMBER, 
	"NAME" VARCHAR2(200 BYTE), 
	"BUILDING_NUMBER" NUMBER, 
	"COLLEGE_ID" NUMBER
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table COLLAGES
--------------------------------------------------------

  CREATE TABLE "MOHAMMED"."COLLAGES" 
   (	"ID" NUMBER, 
	"NAME" VARCHAR2(200 BYTE), 
	"CODE" NUMBER
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table HALLS
--------------------------------------------------------

  CREATE TABLE "MOHAMMED"."HALLS" 
   (	"ID" NUMBER, 
	"NAME" VARCHAR2(200 BYTE), 
	"HALL_NUMBER" NUMBER, 
	"BUILDING_ID" NUMBER
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table PERMISSIONS
--------------------------------------------------------

  CREATE TABLE "MOHAMMED"."PERMISSIONS" 
   (	"ID" NUMBER, 
	"NAME" VARCHAR2(50 BYTE)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table RESERVATIONS
--------------------------------------------------------

  CREATE TABLE "MOHAMMED"."RESERVATIONS" 
   (	"ID" NUMBER, 
	"NAME" VARCHAR2(200 BYTE), 
	"HALL_ID" NUMBER, 
	"RESERVATION_DATE" DATE, 
	"TIME_START" DATE, 
	"TIME_END" DATE, 
	"USER_ID" NUMBER
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table ROLEPERMISSION
--------------------------------------------------------

  CREATE TABLE "MOHAMMED"."ROLEPERMISSION" 
   (	"ID" NUMBER, 
	"ROLEID" NUMBER, 
	"PERMISSIONID" NUMBER
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table SITEROLE
--------------------------------------------------------

  CREATE TABLE "MOHAMMED"."SITEROLE" 
   (	"ID" NUMBER, 
	"NAME" VARCHAR2(200 BYTE), 
	"NORMALIZEDNAME" VARCHAR2(200 BYTE), 
	"DESCRIPTION" VARCHAR2(200 BYTE), 
	"CREATED" DATE
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table SITEUSER
--------------------------------------------------------

  CREATE TABLE "MOHAMMED"."SITEUSER" 
   (	"ID" NUMBER, 
	"USERNAME" VARCHAR2(200 BYTE), 
	"FORENAME" VARCHAR2(200 BYTE), 
	"SURNAME" VARCHAR2(200 BYTE), 
	"NORMALIZEDUSERNAME" VARCHAR2(200 BYTE), 
	"EMAIL" VARCHAR2(200 BYTE), 
	"NORMALIZEDEMAIL" VARCHAR2(200 BYTE), 
	"EMAILCONFIRMED" VARCHAR2(5 BYTE), 
	"PASSWORDHASH" VARCHAR2(200 BYTE), 
	"PHONENUMBER" VARCHAR2(200 BYTE), 
	"PHONENUMBERCONFIRMED" VARCHAR2(5 BYTE), 
	"TWOFACTORENABLED" VARCHAR2(5 BYTE), 
	"CREATED" DATE
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table SITEUSERROLE
--------------------------------------------------------

  CREATE TABLE "MOHAMMED"."SITEUSERROLE" 
   (	"USERID" NUMBER, 
	"ROLEID" NUMBER
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
REM INSERTING into MOHAMMED.BUILDING
SET DEFINE OFF;
Insert into MOHAMMED.BUILDING (ID,NAME,BUILDING_NUMBER,COLLEGE_ID) values (65,'7',7,1);
Insert into MOHAMMED.BUILDING (ID,NAME,BUILDING_NUMBER,COLLEGE_ID) values (35,'11',11,1);
Insert into MOHAMMED.BUILDING (ID,NAME,BUILDING_NUMBER,COLLEGE_ID) values (63,'1',1,1);
Insert into MOHAMMED.BUILDING (ID,NAME,BUILDING_NUMBER,COLLEGE_ID) values (36,'321',123,1);
Insert into MOHAMMED.BUILDING (ID,NAME,BUILDING_NUMBER,COLLEGE_ID) values (37,'33',33,1);
Insert into MOHAMMED.BUILDING (ID,NAME,BUILDING_NUMBER,COLLEGE_ID) values (64,'2',2,1);
Insert into MOHAMMED.BUILDING (ID,NAME,BUILDING_NUMBER,COLLEGE_ID) values (38,'44',44,1);
Insert into MOHAMMED.BUILDING (ID,NAME,BUILDING_NUMBER,COLLEGE_ID) values (39,'0',0,1);
Insert into MOHAMMED.BUILDING (ID,NAME,BUILDING_NUMBER,COLLEGE_ID) values (40,'222',222,1);
Insert into MOHAMMED.BUILDING (ID,NAME,BUILDING_NUMBER,COLLEGE_ID) values (33,'1',1,1);
Insert into MOHAMMED.BUILDING (ID,NAME,BUILDING_NUMBER,COLLEGE_ID) values (34,'12',12,1);
REM INSERTING into MOHAMMED.COLLAGES
SET DEFINE OFF;
Insert into MOHAMMED.COLLAGES (ID,NAME,CODE) values (1,'master',11);
REM INSERTING into MOHAMMED.HALLS
SET DEFINE OFF;
Insert into MOHAMMED.HALLS (ID,NAME,HALL_NUMBER,BUILDING_ID) values (1,'hall1',768,2321);
Insert into MOHAMMED.HALLS (ID,NAME,HALL_NUMBER,BUILDING_ID) values (2,'hall2',223,223);
REM INSERTING into MOHAMMED.PERMISSIONS
SET DEFINE OFF;
Insert into MOHAMMED.PERMISSIONS (ID,NAME) values (1,'halls-Read');
Insert into MOHAMMED.PERMISSIONS (ID,NAME) values (2,'halls-Create');
Insert into MOHAMMED.PERMISSIONS (ID,NAME) values (3,'halls-Update');
Insert into MOHAMMED.PERMISSIONS (ID,NAME) values (4,'halls-Delete');
Insert into MOHAMMED.PERMISSIONS (ID,NAME) values (5,'reservations-Read');
Insert into MOHAMMED.PERMISSIONS (ID,NAME) values (6,'reservations-Create');
Insert into MOHAMMED.PERMISSIONS (ID,NAME) values (7,'reservations-Update');
Insert into MOHAMMED.PERMISSIONS (ID,NAME) values (8,'reservations-Delete');
Insert into MOHAMMED.PERMISSIONS (ID,NAME) values (9,'buildings-Read');
Insert into MOHAMMED.PERMISSIONS (ID,NAME) values (10,'buildings-Create');
Insert into MOHAMMED.PERMISSIONS (ID,NAME) values (11,'buildings-Update');
Insert into MOHAMMED.PERMISSIONS (ID,NAME) values (12,'buildings-Delete');
Insert into MOHAMMED.PERMISSIONS (ID,NAME) values (13,'colleges-Read');
Insert into MOHAMMED.PERMISSIONS (ID,NAME) values (14,'colleges-Create');
Insert into MOHAMMED.PERMISSIONS (ID,NAME) values (15,'colleges-Update');
Insert into MOHAMMED.PERMISSIONS (ID,NAME) values (16,'colleges-Delete');
REM INSERTING into MOHAMMED.RESERVATIONS
SET DEFINE OFF;
REM INSERTING into MOHAMMED.ROLEPERMISSION
SET DEFINE OFF;
REM INSERTING into MOHAMMED.SITEROLE
SET DEFINE OFF;
Insert into MOHAMMED.SITEROLE (ID,NAME,NORMALIZEDNAME,DESCRIPTION,CREATED) values (2,'super_admin','SUPER_ADMIN','system_super_adminstrator',to_date('12-SEP-23','DD-MON-RR'));
Insert into MOHAMMED.SITEROLE (ID,NAME,NORMALIZEDNAME,DESCRIPTION,CREATED) values (3,'user','USER','11111111111111111111',to_date('12-SEP-23','DD-MON-RR'));
Insert into MOHAMMED.SITEROLE (ID,NAME,NORMALIZEDNAME,DESCRIPTION,CREATED) values (4,'supervisor','SUPERVISOR','supervisor22222',to_date('15-SEP-23','DD-MON-RR'));
Insert into MOHAMMED.SITEROLE (ID,NAME,NORMALIZEDNAME,DESCRIPTION,CREATED) values (5,'test','TEST','to make some tests in the system ',to_date('26-SEP-23','DD-MON-RR'));
REM INSERTING into MOHAMMED.SITEUSER
SET DEFINE OFF;
Insert into MOHAMMED.SITEUSER (ID,USERNAME,FORENAME,SURNAME,NORMALIZEDUSERNAME,EMAIL,NORMALIZEDEMAIL,EMAILCONFIRMED,PASSWORDHASH,PHONENUMBER,PHONENUMBERCONFIRMED,TWOFACTORENABLED,CREATED) values (2,'m@m.com','MOHAMMED','MOHAMMED','M@M.COM','m@m.com','M@M.COM','false','AQAAAAEAACcQAAAAEDi2ptQWTp7OI4jVYahjTFmpzh6xx0PAKKxGTdSrAZi0Uims3puH3zhsls+eGyi08Q==','0551316566','false','false',to_date('12-OCT-23','DD-MON-RR'));
Insert into MOHAMMED.SITEUSER (ID,USERNAME,FORENAME,SURNAME,NORMALIZEDUSERNAME,EMAIL,NORMALIZEDEMAIL,EMAILCONFIRMED,PASSWORDHASH,PHONENUMBER,PHONENUMBERCONFIRMED,TWOFACTORENABLED,CREATED) values (36,'a@a.com','a','a','A@A.COM','a@a.com','A@A.COM','false','AQAAAAEAACcQAAAAEHuLPqZO1BjsWyTIhOMbZhbPSgVrRgXKJ9ZrDzBTLhvXJIQd9mosfmu8FqhI8Rj+/w==','22222222','false','false',to_date('15-OCT-23','DD-MON-RR'));
Insert into MOHAMMED.SITEUSER (ID,USERNAME,FORENAME,SURNAME,NORMALIZEDUSERNAME,EMAIL,NORMALIZEDEMAIL,EMAILCONFIRMED,PASSWORDHASH,PHONENUMBER,PHONENUMBERCONFIRMED,TWOFACTORENABLED,CREATED) values (4,'n@n.com','hanan','hanan','N@N.COM','n@n.com','N@N.COM','false','AQAAAAEAACcQAAAAEBBwH6FqszwkkYUjf+kr8axm+ruKMk4+nwrr9nwNaGd7oPDt752dtY+yU0drYrqYAw==','67738','false','false',to_date('15-OCT-23','DD-MON-RR'));
REM INSERTING into MOHAMMED.SITEUSERROLE
SET DEFINE OFF;
Insert into MOHAMMED.SITEUSERROLE (USERID,ROLEID) values (2,2);
Insert into MOHAMMED.SITEUSERROLE (USERID,ROLEID) values (3,2);
Insert into MOHAMMED.SITEUSERROLE (USERID,ROLEID) values (7,4);
Insert into MOHAMMED.SITEUSERROLE (USERID,ROLEID) values (8,3);
Insert into MOHAMMED.SITEUSERROLE (USERID,ROLEID) values (9,3);
Insert into MOHAMMED.SITEUSERROLE (USERID,ROLEID) values (10,3);
Insert into MOHAMMED.SITEUSERROLE (USERID,ROLEID) values (14,3);
Insert into MOHAMMED.SITEUSERROLE (USERID,ROLEID) values (4,3);
--------------------------------------------------------
--  DDL for Index BUILDING_PK
--------------------------------------------------------

  CREATE INDEX "MOHAMMED"."BUILDING_PK" ON "MOHAMMED"."BUILDING" ("NAME") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Index COLLAGES_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "MOHAMMED"."COLLAGES_PK" ON "MOHAMMED"."COLLAGES" ("ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Index HALLS_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "MOHAMMED"."HALLS_PK" ON "MOHAMMED"."HALLS" ("ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Index RESERVATIONS_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "MOHAMMED"."RESERVATIONS_PK" ON "MOHAMMED"."RESERVATIONS" ("ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Index SITEROLE_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "MOHAMMED"."SITEROLE_PK" ON "MOHAMMED"."SITEROLE" ("ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Index SITEUSERROLE_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "MOHAMMED"."SITEUSERROLE_PK" ON "MOHAMMED"."SITEUSERROLE" ("USERID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Index SITEUSER_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "MOHAMMED"."SITEUSER_PK" ON "MOHAMMED"."SITEUSER" ("ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Index BUILDING_PK
--------------------------------------------------------

  CREATE INDEX "MOHAMMED"."BUILDING_PK" ON "MOHAMMED"."BUILDING" ("NAME") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Index COLLAGES_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "MOHAMMED"."COLLAGES_PK" ON "MOHAMMED"."COLLAGES" ("ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Index HALLS_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "MOHAMMED"."HALLS_PK" ON "MOHAMMED"."HALLS" ("ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Index RESERVATIONS_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "MOHAMMED"."RESERVATIONS_PK" ON "MOHAMMED"."RESERVATIONS" ("ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Index SITEROLE_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "MOHAMMED"."SITEROLE_PK" ON "MOHAMMED"."SITEROLE" ("ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Index SITEUSER_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "MOHAMMED"."SITEUSER_PK" ON "MOHAMMED"."SITEUSER" ("ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Index SITEUSERROLE_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "MOHAMMED"."SITEUSERROLE_PK" ON "MOHAMMED"."SITEUSERROLE" ("USERID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Trigger BUILDING_TRG
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "MOHAMMED"."BUILDING_TRG" 
BEFORE INSERT ON BUILDING 
FOR EACH ROW 
BEGIN
  <<COLUMN_SEQUENCES>>
  BEGIN
    NULL;
  END COLUMN_SEQUENCES;
END;


/
ALTER TRIGGER "MOHAMMED"."BUILDING_TRG" ENABLE;
--------------------------------------------------------
--  DDL for Trigger BUILDING_TRG1
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "MOHAMMED"."BUILDING_TRG1" 
BEFORE INSERT ON BUILDING 
FOR EACH ROW 
BEGIN
  <<COLUMN_SEQUENCES>>
  BEGIN
    IF INSERTING AND :NEW.ID IS NULL THEN
      SELECT BUILDING_SEQ1.NEXTVAL INTO :NEW.ID FROM SYS.DUAL;
    END IF;
  END COLUMN_SEQUENCES;
END;


/
ALTER TRIGGER "MOHAMMED"."BUILDING_TRG1" ENABLE;
--------------------------------------------------------
--  DDL for Trigger COLLAGES_TRG
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "MOHAMMED"."COLLAGES_TRG" 
BEFORE INSERT ON COLLAGES 
FOR EACH ROW 
BEGIN
  <<COLUMN_SEQUENCES>>
  BEGIN
    IF INSERTING AND :NEW.ID IS NULL THEN
      SELECT COLLAGES_SEQ.NEXTVAL INTO :NEW.ID FROM SYS.DUAL;
    END IF;
  END COLUMN_SEQUENCES;
END;


/
ALTER TRIGGER "MOHAMMED"."COLLAGES_TRG" ENABLE;
--------------------------------------------------------
--  DDL for Trigger PERMISSIONS_TRG
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "MOHAMMED"."PERMISSIONS_TRG" 
BEFORE INSERT ON PERMISSIONS 
FOR EACH ROW 
BEGIN
  <<COLUMN_SEQUENCES>>
  BEGIN
    IF INSERTING AND :NEW.ID IS NULL THEN
      SELECT PERMISSIONS_SEQ.NEXTVAL INTO :NEW.ID FROM SYS.DUAL;
    END IF;
  END COLUMN_SEQUENCES;
END;


/
ALTER TRIGGER "MOHAMMED"."PERMISSIONS_TRG" ENABLE;
--------------------------------------------------------
--  DDL for Trigger ROLEPERMISSION_TRG
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "MOHAMMED"."ROLEPERMISSION_TRG" 
BEFORE INSERT ON ROLEPERMISSION 
FOR EACH ROW 
BEGIN
  <<COLUMN_SEQUENCES>>
  BEGIN
    IF INSERTING AND :NEW.ID IS NULL THEN
      SELECT ROLEPERMISSION_SEQ.NEXTVAL INTO :NEW.ID FROM SYS.DUAL;
    END IF;
  END COLUMN_SEQUENCES;
END;


/
ALTER TRIGGER "MOHAMMED"."ROLEPERMISSION_TRG" ENABLE;
--------------------------------------------------------
--  DDL for Trigger SITEUSER_TRG
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "MOHAMMED"."SITEUSER_TRG" 
BEFORE INSERT ON SITEUSER 
FOR EACH ROW 
BEGIN
  <<COLUMN_SEQUENCES>>
  BEGIN
    NULL;
  END COLUMN_SEQUENCES;
END;


/
ALTER TRIGGER "MOHAMMED"."SITEUSER_TRG" ENABLE;
--------------------------------------------------------
--  DDL for Trigger SITEUSER_TRG1
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "MOHAMMED"."SITEUSER_TRG1" 
BEFORE INSERT ON SITEUSER 
FOR EACH ROW 
BEGIN
  <<COLUMN_SEQUENCES>>
  BEGIN
    IF INSERTING AND :NEW.ID IS NULL THEN
      SELECT SITEUSER_SEQ1.NEXTVAL INTO :NEW.ID FROM SYS.DUAL;
    END IF;
  END COLUMN_SEQUENCES;
END;


/
ALTER TRIGGER "MOHAMMED"."SITEUSER_TRG1" ENABLE;
--------------------------------------------------------
--  DDL for Trigger BUILDING_TRG
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "MOHAMMED"."BUILDING_TRG" 
BEFORE INSERT ON BUILDING 
FOR EACH ROW 
BEGIN
  <<COLUMN_SEQUENCES>>
  BEGIN
    NULL;
  END COLUMN_SEQUENCES;
END;


/
ALTER TRIGGER "MOHAMMED"."BUILDING_TRG" ENABLE;
--------------------------------------------------------
--  DDL for Trigger BUILDING_TRG1
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "MOHAMMED"."BUILDING_TRG1" 
BEFORE INSERT ON BUILDING 
FOR EACH ROW 
BEGIN
  <<COLUMN_SEQUENCES>>
  BEGIN
    IF INSERTING AND :NEW.ID IS NULL THEN
      SELECT BUILDING_SEQ1.NEXTVAL INTO :NEW.ID FROM SYS.DUAL;
    END IF;
  END COLUMN_SEQUENCES;
END;


/
ALTER TRIGGER "MOHAMMED"."BUILDING_TRG1" ENABLE;
--------------------------------------------------------
--  DDL for Trigger COLLAGES_TRG
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "MOHAMMED"."COLLAGES_TRG" 
BEFORE INSERT ON COLLAGES 
FOR EACH ROW 
BEGIN
  <<COLUMN_SEQUENCES>>
  BEGIN
    IF INSERTING AND :NEW.ID IS NULL THEN
      SELECT COLLAGES_SEQ.NEXTVAL INTO :NEW.ID FROM SYS.DUAL;
    END IF;
  END COLUMN_SEQUENCES;
END;


/
ALTER TRIGGER "MOHAMMED"."COLLAGES_TRG" ENABLE;
--------------------------------------------------------
--  DDL for Trigger PERMISSIONS_TRG
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "MOHAMMED"."PERMISSIONS_TRG" 
BEFORE INSERT ON PERMISSIONS 
FOR EACH ROW 
BEGIN
  <<COLUMN_SEQUENCES>>
  BEGIN
    IF INSERTING AND :NEW.ID IS NULL THEN
      SELECT PERMISSIONS_SEQ.NEXTVAL INTO :NEW.ID FROM SYS.DUAL;
    END IF;
  END COLUMN_SEQUENCES;
END;


/
ALTER TRIGGER "MOHAMMED"."PERMISSIONS_TRG" ENABLE;
--------------------------------------------------------
--  DDL for Trigger ROLEPERMISSION_TRG
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "MOHAMMED"."ROLEPERMISSION_TRG" 
BEFORE INSERT ON ROLEPERMISSION 
FOR EACH ROW 
BEGIN
  <<COLUMN_SEQUENCES>>
  BEGIN
    IF INSERTING AND :NEW.ID IS NULL THEN
      SELECT ROLEPERMISSION_SEQ.NEXTVAL INTO :NEW.ID FROM SYS.DUAL;
    END IF;
  END COLUMN_SEQUENCES;
END;


/
ALTER TRIGGER "MOHAMMED"."ROLEPERMISSION_TRG" ENABLE;
--------------------------------------------------------
--  DDL for Trigger SITEUSER_TRG
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "MOHAMMED"."SITEUSER_TRG" 
BEFORE INSERT ON SITEUSER 
FOR EACH ROW 
BEGIN
  <<COLUMN_SEQUENCES>>
  BEGIN
    NULL;
  END COLUMN_SEQUENCES;
END;


/
ALTER TRIGGER "MOHAMMED"."SITEUSER_TRG" ENABLE;
--------------------------------------------------------
--  DDL for Trigger SITEUSER_TRG1
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "MOHAMMED"."SITEUSER_TRG1" 
BEFORE INSERT ON SITEUSER 
FOR EACH ROW 
BEGIN
  <<COLUMN_SEQUENCES>>
  BEGIN
    IF INSERTING AND :NEW.ID IS NULL THEN
      SELECT SITEUSER_SEQ1.NEXTVAL INTO :NEW.ID FROM SYS.DUAL;
    END IF;
  END COLUMN_SEQUENCES;
END;


/
ALTER TRIGGER "MOHAMMED"."SITEUSER_TRG1" ENABLE;
--------------------------------------------------------
--  DDL for Procedure BUILDING_CREATE
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "MOHAMMED"."BUILDING_CREATE" (
BName NVARCHAR2,
BNumber NUMBER,
BCollege_Id NUMBER ,

qres  out NVARCHAR2 
)
AS 
        ident number ;

BEGIN
        SELECT COUNT (*)
        INTO ident FROM BUILDING WHERE (college_id = BCollege_ID AND building_number = BNumber );

        IF        ident = 0
        THEN
                  INSERT INTO building( name , building_number , college_id ) VALUES( BName, BNumber, BCollege_Id);
                   qres := 'success';
        ELSE
                    qres := 'fail';
        END IF;

END BUILDING_CREATE;

/
--------------------------------------------------------
--  DDL for Procedure COLLAGES_GETALL
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "MOHAMMED"."COLLAGES_GETALL" 
(res out SYS_REFCURSOR)
AS
BEGIN
         OPEN RES FOR SELECT ID,NAME,Code FROM Collages ;
END;



/
--------------------------------------------------------
--  DDL for Procedure DASHBORD_TODAY_RESERVATIONS
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "MOHAMMED"."DASHBORD_TODAY_RESERVATIONS" ( 
  res out SYS_REFCURSOR
) AS 
BEGIN
  OPEN res FOR SELECT  COUNT(DISTINCT(ID))  AS total from halls;

END DASHBORD_TODAY_RESERVATIONS;



/
--------------------------------------------------------
--  DDL for Procedure GETHALLS
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "MOHAMMED"."GETHALLS" 
(
  --ID IN NUMBER ,
  
  res out SYS_REFCURSOR
) AS 
BEGIN
    OPEN res FOR SELECT * FROM halls ;

END GETHALLS;



/
--------------------------------------------------------
--  DDL for Procedure GETHALLSBYID
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "MOHAMMED"."GETHALLSBYID" 
(
  hallid IN NUMBER ,
  
  res out SYS_REFCURSOR
) AS 
BEGIN
    OPEN res FOR SELECT * FROM halls Where id = hallid;

END gethallsbyid;



/
--------------------------------------------------------
--  DDL for Procedure GET_COLLEGE_BUILDING
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "MOHAMMED"."GET_COLLEGE_BUILDING" 
   (res out SYS_REFCURSOR)
AS
BEGIN
  OPEN RES FOR 
        SELECT Building.ID,Building.Name AS buldingName ,Building.building_Number AS buldingnumber,Collages.Name AS BuldingCollageName
		FROM Building join Collages
		On (Building.College_Id = Collages.ID) ;
END GET_COLLEGE_BUILDING;



/
--------------------------------------------------------
--  DDL for Procedure GET_HALL_BUILDING
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "MOHAMMED"."GET_HALL_BUILDING" 
(
    res out SYS_REFCURSOR
) AS 
BEGIN
  OPEN res FOR 
    SELECT halls.ID AS Id , halls.NAME AS HallName ,halls.HALL_NUMBER AS HallNumber ,
        Building.Id AS Building_ID, Building.Name AS BuildingName ,Building.BUILDING_NUMBER AS BuildingNumber
	FROM halls JOIN Building
		ON (halls.Building_ID = Building.ID);
        
END GET_HALL_BUILDING;

/
--------------------------------------------------------
--  DDL for Procedure IDENTITY_ADDUSERTOROLE
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "MOHAMMED"."IDENTITY_ADDUSERTOROLE" (
  "U_ID" IN NUMBER, 
  "R_ID" IN NUMBER) 
  
  IS
       VarRowNum  NUMBER := 0 ;
BEGIN 
       SELECT COUNT("ROLEID")

       INTO   VarRowNum

	   FROM   SiteUserRole

	  WHERE  UserId = "U_ID" AND RoleId = "R_ID" ;

       IF VarRowNum = 0 THEN
         INSERT INTO SiteUserRole
			(UserId, RoleId	)
			VALUES
			("U_ID","R_ID") ;

       END IF;
END;


/
--------------------------------------------------------
--  DDL for Procedure IDENTITY_FINDBYEMAIL
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "MOHAMMED"."IDENTITY_FINDBYEMAIL" 
(
  NORUSEREMAIL IN VARCHAR2 ,
  res OUT SYS_REFCURSOR
) AS 
BEGIN

    OPEN res FOR  SELECT * FROM SiteUser
    WHERE NormalizedEmail = NORUSEREMAIL;
END IDENTITY_FINDBYEMAIL;


/
--------------------------------------------------------
--  DDL for Procedure IDENTITY_FINDBYID
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "MOHAMMED"."IDENTITY_FINDBYID" 
(
  U_ID IN NUMBER,
  res out SYS_REFCURSOR
) AS 
BEGIN
    OPEN res FOR 
        SELECT * FROM SiteUser
        WHERE Id = U_ID;
END IDENTITY_FINDBYID;

/
--------------------------------------------------------
--  DDL for Procedure IDENTITY_FINDBYNAME
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "MOHAMMED"."IDENTITY_FINDBYNAME" 
(
  normalizedname in varchar2 ,
  res out SYS_REFCURSOR
) as 
begin

OPEN res FOR 
SELECT * FROM SiteUser
    WHERE NormalizedUserName = normalizedname ;

end identity_findbyname;


/
--------------------------------------------------------
--  DDL for Procedure IDENTITY_FINDROLEBYID
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "MOHAMMED"."IDENTITY_FINDROLEBYID" 
(
  R_ID IN NUMBER,
  res out SYS_REFCURSOR
) AS 
BEGIN
  OPEN res FOR 
    SELECT  *  FROM SiteRole
    WHERE Id = R_ID;
        
END IDENTITY_FINDROLEBYID;

/
--------------------------------------------------------
--  DDL for Procedure IDENTITY_FINDROLEBYNAME
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "MOHAMMED"."IDENTITY_FINDROLEBYNAME" 
(
    NorRoleName IN VARCHAR2, 
    res OUT SYS_REFCURSOR
) AS 
BEGIN

    OPEN res FOR SELECT  * FROM SiteRole
    WHERE NormalizedName = NorRoleName;

END IDENTITY_FINDROLEBYNAME;


/
--------------------------------------------------------
--  DDL for Procedure IDENTITY_GETALLROLES
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "MOHAMMED"."IDENTITY_GETALLROLES" (res out SYS_REFCURSOR) AS

BEGIN
  OPEN RES FOR SELECT SiteRole.Id,SiteRole.Name,SiteRole.Description FROM SiteRole ;
END IDENTITY_GETALLROLES;



/
--------------------------------------------------------
--  DDL for Procedure IDENTITY_GETALLUSERS
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "MOHAMMED"."IDENTITY_GETALLUSERS" 
(
    res out SYS_REFCURSOR
) AS 
BEGIN
  OPEN res FOR SELECT * FROM SiteUser;
END IDENTITY_GETALLUSERS;

/
--------------------------------------------------------
--  DDL for Procedure IDENTITY_GETUSERROLES
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "MOHAMMED"."IDENTITY_GETUSERROLES" 
(
  U_ID IN NUMBER,
  res out SYS_REFCURSOR
) AS 
BEGIN
  OPEN res FOR 
    SELECT r.* FROM SiteRole r 
    INNER JOIN SiteUserRole ur ON ur.RoleId = r.Id 
    WHERE ur.UserId = U_ID;

END IDENTITY_GETUSERROLES;

/
--------------------------------------------------------
--  DDL for Procedure IDENTITY_GETUSERROLESBYUSERID
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "MOHAMMED"."IDENTITY_GETUSERROLESBYUSERID" 
(
  USERID IN NUMBER, 
  res out SYS_REFCURSOR
) AS 
BEGIN
    OPEN res FOR SELECT r.Name FROM SiteRole r 
    INNER JOIN SiteUserRole ur ON ur.RoleId = r.Id 
    WHERE ur.UserId = UserId ;
END IDENTITY_GETUSERROLESBYUSERID;


/
--------------------------------------------------------
--  DDL for Procedure IDENTITY_INSERTUSER
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "MOHAMMED"."IDENTITY_INSERTUSER" 
(
  USERNAME IN VARCHAR2 
, NORMALIZEDUSERNAME IN VARCHAR2 
, FORENAME IN VARCHAR2 
, SURNAME IN VARCHAR2 
, EMAIL IN VARCHAR2 
, NORMALIZEDEMAIL IN VARCHAR2 
, EMAILCONFIRMED IN VARCHAR2 
, PASSWORDHASH IN VARCHAR2 
, PHONENUMBER IN VARCHAR2 
, PHONENUMBERCONFIRMED IN VARCHAR2 
, TWOFACTORENABLED IN VARCHAR2 
, CREATED IN DATE
, qres out VARCHAR2
) 
AS 
    a VARCHAR2(5) := '' ;
    b VARCHAR2(5) := '' ;
    c VARCHAR2(5) := '' ;
BEGIN
    if EMAILCONFIRMED = 0  then 
        a := 'false';
        else
        a := 'true';
    end if;

    if PHONENUMBERCONFIRMED = 0  then 
        b := 'false';
         else
        b := 'true';
    end if;

    if TWOFACTORENABLED = 0  then 
        c := 'false';
        else
        c := 'true';
    end if;

INSERT INTO SiteUser 
(
	UserName, 
    NormalizedUserName, 
    Email,
    NormalizedEmail, 
    EmailConfirmed,
    Forename,
    Surname,
    PasswordHash,
    PhoneNumber, 
    PhoneNumberConfirmed, 
    TwoFactorEnabled,
	Created
 )
 VALUES
 (
	Username,
	NormalizedUserName,
	Email,
	NormalizedEmail,
	a,
	Forename,
	Surname,
	PasswordHash,
	PhoneNumber,
	b,
	c,
	TO_DATE (Created)
 );
qres := 'success'; 

END IDENTITY_INSERTUSER;


/
--------------------------------------------------------
--  DDL for Procedure IDENTITY_ISUSERINROLE
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "MOHAMMED"."IDENTITY_ISUSERINROLE" 
(
  R_ID IN NUMBER 
, U_ID IN NUMBER 
, res OUT SYS_REFCURSOR
) AS 
BEGIN
  OPEN res FOR 
    SELECT COUNT(*) FROM SiteUserRole 
    WHERE UserId = R_ID 
    AND RoleId = U_ID;

END IDENTITY_ISUSERINROLE;


/
--------------------------------------------------------
--  DDL for Procedure IDENTITY_UPDATEUSER
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "MOHAMMED"."IDENTITY_UPDATEUSER" 
(
  U_ID IN NUMBER   
, USERNAME IN VARCHAR2 
, NORMALIZEDUSERNAME IN VARCHAR2 
, FORENAME IN VARCHAR2 
, SURNAME IN VARCHAR2 
, EMAIL IN VARCHAR2 
, NORMALIZEDEMAIL IN VARCHAR2 
, EMAILCONFIRMED IN VARCHAR2 
, PASSWORDHASH IN VARCHAR2 
, PHONENUMBER IN VARCHAR2 
, PHONENUMBERCONFIRMED IN VARCHAR2 
, TWOFACTORENABLED IN VARCHAR2 
, qres out NVARCHAR2
) AS 
 a VARCHAR2(5) := '' ;
    b VARCHAR2(5) := '' ;
    c VARCHAR2(5) := '' ;
BEGIN
    if EMAILCONFIRMED = 0  then 
        a := 'false';
        else
        a := 'true';
    end if;

    if PHONENUMBERCONFIRMED = 0  then 
        b := 'false';
         else
        b := 'true';
    end if;

    if TWOFACTORENABLED = 0  then 
        c := 'false';
        else
        c := 'true';
    end if;
 
UPDATE SiteUser SET

	UserName = Username,
    NormalizedUserName = NormalizedUserName,
    Email = Email,
    NormalizedEmail = NormalizedEmail, 
    EmailConfirmed =EmailConfirmed,
    Forename = Forename,
    Surname = Surname,
    PasswordHash = PasswordHash,
    PhoneNumber = PhoneNumber,
    PhoneNumberConfirmed = PhoneNumberConfirmed,
    TwoFactorEnabled = TwoFactorEnabled 

WHERE Id = U_ID ;
 qres := 'success' ;
 
END IDENTITY_UPDATEUSER;

/
--------------------------------------------------------
--  DDL for Procedure PERMISSIONS_GETBYNAME
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "MOHAMMED"."PERMISSIONS_GETBYNAME" 
(
  PERMISSIONNAME IN VARCHAR2 ,
  res out SYS_REFCURSOR
) AS 
BEGIN
	OPEN res FOR SELECT * FROM Permissions WHERE (Permissions.Name = PermissionName);
END PERMISSIONS_GETBYNAME;

/
--------------------------------------------------------
--  DDL for Procedure PERMISSION_GET_ALL_BY_ROLE
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "MOHAMMED"."PERMISSION_GET_ALL_BY_ROLE" 
(
  ROLE_ID IN NUMBER,
  res out SYS_REFCURSOR
) AS 
BEGIN
		OPEN res FOR SELECT * FROM RolePermission WHERE (RoleID = ROLE_ID);
END PERMISSION_GET_ALL_BY_ROLE;

/
--------------------------------------------------------
--  DDL for Procedure PERMISSION_GET_BY_ID
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "MOHAMMED"."PERMISSION_GET_BY_ID" 
(
  PERM_ID IN NUMBER
  , res out SYS_REFCURSOR
) AS 
BEGIN
  OPEN res FOR SELECT * FROM Permissions WHERE (ID = PERM_ID);
  
END PERMISSION_GET_BY_ID;

/
--------------------------------------------------------
--  DDL for Procedure PERMISSION_USERHASPERMISSION
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "MOHAMMED"."PERMISSION_USERHASPERMISSION" 
(
  ROLE_ID IN NUMBER 
, PERMISSION_ID IN NUMBER 
, res out SYS_REFCURSOR
) AS 
BEGIN
  	OPEn res FOR SELECT * FROM RolePermission 
    WHERE(RoleID =ROLE_ID AND PermissionID =PERMISSION_ID);
END PERMISSION_USERHASPERMISSION;

/
--------------------------------------------------------
--  Constraints for Table BUILDING
--------------------------------------------------------

  ALTER TABLE "MOHAMMED"."BUILDING" MODIFY ("ID" NOT NULL ENABLE);
  ALTER TABLE "MOHAMMED"."BUILDING" MODIFY ("NAME" NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table COLLAGES
--------------------------------------------------------

  ALTER TABLE "MOHAMMED"."COLLAGES" ADD CONSTRAINT "COLLAGES_PK" PRIMARY KEY ("ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE;
--------------------------------------------------------
--  Constraints for Table HALLS
--------------------------------------------------------

  ALTER TABLE "MOHAMMED"."HALLS" ADD CONSTRAINT "HALLS_PK" PRIMARY KEY ("ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE;
--------------------------------------------------------
--  Constraints for Table PERMISSIONS
--------------------------------------------------------

  ALTER TABLE "MOHAMMED"."PERMISSIONS" MODIFY ("ID" NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table RESERVATIONS
--------------------------------------------------------

  ALTER TABLE "MOHAMMED"."RESERVATIONS" MODIFY ("ID" NOT NULL ENABLE);
  ALTER TABLE "MOHAMMED"."RESERVATIONS" ADD CONSTRAINT "RESERVATIONS_PK" PRIMARY KEY ("ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE;
--------------------------------------------------------
--  Constraints for Table ROLEPERMISSION
--------------------------------------------------------

  ALTER TABLE "MOHAMMED"."ROLEPERMISSION" MODIFY ("ID" NOT NULL ENABLE);
  ALTER TABLE "MOHAMMED"."ROLEPERMISSION" MODIFY ("ROLEID" NOT NULL ENABLE);
  ALTER TABLE "MOHAMMED"."ROLEPERMISSION" MODIFY ("PERMISSIONID" NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table SITEROLE
--------------------------------------------------------

  ALTER TABLE "MOHAMMED"."SITEROLE" MODIFY ("ID" NOT NULL ENABLE);
  ALTER TABLE "MOHAMMED"."SITEROLE" ADD CONSTRAINT "SITEROLE_PK" PRIMARY KEY ("ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE;
--------------------------------------------------------
--  Constraints for Table SITEUSER
--------------------------------------------------------

  ALTER TABLE "MOHAMMED"."SITEUSER" MODIFY ("ID" NOT NULL ENABLE);
  ALTER TABLE "MOHAMMED"."SITEUSER" MODIFY ("SURNAME" NOT NULL ENABLE);
  ALTER TABLE "MOHAMMED"."SITEUSER" MODIFY ("EMAIL" NOT NULL ENABLE);
  ALTER TABLE "MOHAMMED"."SITEUSER" MODIFY ("NORMALIZEDEMAIL" NOT NULL ENABLE);
  ALTER TABLE "MOHAMMED"."SITEUSER" MODIFY ("PASSWORDHASH" NOT NULL ENABLE);
  ALTER TABLE "MOHAMMED"."SITEUSER" MODIFY ("PHONENUMBER" NOT NULL ENABLE);
  ALTER TABLE "MOHAMMED"."SITEUSER" ADD CONSTRAINT "SITEUSER_PK" PRIMARY KEY ("ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE;
--------------------------------------------------------
--  Constraints for Table SITEUSERROLE
--------------------------------------------------------

  ALTER TABLE "MOHAMMED"."SITEUSERROLE" MODIFY ("USERID" NOT NULL ENABLE);
  ALTER TABLE "MOHAMMED"."SITEUSERROLE" ADD CONSTRAINT "SITEUSERROLE_PK" PRIMARY KEY ("USERID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE;
