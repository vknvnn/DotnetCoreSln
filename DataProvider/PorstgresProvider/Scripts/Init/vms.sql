-- step 1: check existed db 
-- SELECT 1 FROM pg_database WHERE datname='360vms';
-- step 2: if existed then drop db.
-- drop database 360vms;
-- step 3: create new db;
--create database 360vms;

/*drop table public."TableName";
create table public."TableName"
(
	"Id" bigserial NOT NULL,
    "CreatedDate" timestamp with time zone NOT NULL,
    "ModifiedDate" timestamp with time zone NOT NULL,
    "CreatedBy" character varying(20) NOT NULL,
    "ModifiedBy" character varying(20) NOT NULL,
    "Version" int not null,
    "TenantId" uuid not null,
    CONSTRAINT student_pkey PRIMARY KEY ("Id")
);
ALTER TABLE public."Student"
    OWNER to postgres;

GRANT ALL ON TABLE public."Student" TO postgres;

-- Index: tenant_id
CREATE INDEX created_by
    ON public."Student" USING btree
    ("CreatedBy" COLLATE pg_catalog."default" varchar_ops)
    TABLESPACE pg_default;
CREATE INDEX modified_by
    ON public."Student" USING btree
    ("ModifiedBy" COLLATE pg_catalog."default" varchar_ops)
    TABLESPACE pg_default;
CREATE INDEX tenant_id
    ON public."Student" USING btree
    ("TenantId")
    TABLESPACE pg_default;
*/

--- Create Table Version
create table public."Version"
(
	"Id" bigserial NOT NULL,
    "CreatedDate" timestamp with time zone NOT NULL,
    "ModifiedDate" timestamp with time zone NOT NULL,
    "CreatedBy" character varying(20) NOT NULL,
    "ModifiedBy" character varying(20) NOT NULL,
    "Version" int not null,
    "TenantId" uuid not null,
	"VersionName" character varying(10) NOT NULL,
    CONSTRAINT student_pkey PRIMARY KEY ("Id")
);
ALTER TABLE public."Version"
    OWNER to postgres;

GRANT ALL ON TABLE public."Version" TO postgres;

