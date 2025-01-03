-- Table: public.Factories

-- DROP TABLE IF EXISTS public."Factories";

CREATE TABLE IF NOT EXISTS public."Factories"
(
    id integer NOT NULL DEFAULT nextval('factories_id_seq'::regclass),
    name character varying(50) COLLATE pg_catalog."default",
    description text COLLATE pg_catalog."default",
    production character varying(60) COLLATE pg_catalog."default",
    createdat timestamp without time zone,
    CONSTRAINT factories_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Factories"
    OWNER to postgres;