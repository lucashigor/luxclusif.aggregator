-- Table: public.TotalExpendedInOrdersAggregated

-- DROP TABLE IF EXISTS public."TotalExpendedInOrdersAggregated";

CREATE TABLE IF NOT EXISTS public."TotalExpendedInOrdersAggregated"
(
    "Name" character varying(100) COLLATE pg_catalog."default" NOT NULL,
    "Id" uuid NOT NULL,
    "UserId" uuid NOT NULL,
    "Value" money NOT NULL,
	"CreatedAt" timestamp with time zone NOT NULL,
    "LastUpdateAt" timestamp with time zone 
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."TotalExpendedInOrdersAggregated"
    OWNER to postgres;
-- Index: Id_index

-- DROP INDEX IF EXISTS public."Id_index";

CREATE UNIQUE INDEX IF NOT EXISTS "Id_index"
    ON public."TotalExpendedInOrdersAggregated" USING btree
    ("Id" ASC NULLS LAST)
    INCLUDE("Id")
    TABLESPACE pg_default;