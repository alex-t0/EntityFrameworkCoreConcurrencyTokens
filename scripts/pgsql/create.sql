CREATE TABLE "public"."AwesomeEntity" (
	"Uid" uuid NOT NULL,
	"Name" varchar NULL
);

ALTER TABLE public."AwesomeEntity" ADD CONSTRAINT "AwesomeEntity_pk" PRIMARY KEY ("Uid");

ALTER SCHEMA public RENAME TO dbo;
