--
-- PostgreSQL database cluster dump
--

-- Started on 2024-05-31 22:09:51

SET default_transaction_read_only = off;

SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;

--
-- Roles
--

CREATE ROLE postgres;
ALTER ROLE postgres WITH SUPERUSER INHERIT CREATEROLE CREATEDB LOGIN REPLICATION BYPASSRLS;

--
-- User Configurations
--








--
-- Databases
--

--
-- Database "template1" dump
--

\connect template1

--
-- PostgreSQL database dump
--

-- Dumped from database version 16.3
-- Dumped by pg_dump version 16.3

-- Started on 2024-05-31 22:09:51

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

-- Completed on 2024-05-31 22:09:52

--
-- PostgreSQL database dump complete
--

--
-- Database "mockup" dump
--

--
-- PostgreSQL database dump
--

-- Dumped from database version 16.3
-- Dumped by pg_dump version 16.3

-- Started on 2024-05-31 22:09:52

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 4844 (class 1262 OID 16398)
-- Name: mockup; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE mockup WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Spanish_Mexico.1252';


ALTER DATABASE mockup OWNER TO postgres;

\connect mockup

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 219 (class 1255 OID 16471)
-- Name: create_invoice(character varying, timestamp with time zone, character varying, character varying); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.create_invoice(IN name character varying, IN date timestamp with time zone, IN amount character varying, IN file character varying)
    LANGUAGE plpgsql
    AS $$
BEGIN
  INSERT INTO invoices (name, date, amount, file)
  VALUES (name, date, amount, file);
END;
$$;


ALTER PROCEDURE public.create_invoice(IN name character varying, IN date timestamp with time zone, IN amount character varying, IN file character varying) OWNER TO postgres;

--
-- TOC entry 221 (class 1255 OID 16473)
-- Name: delete_invoice(numeric); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.delete_invoice(IN _id numeric)
    LANGUAGE plpgsql
    AS $$
	BEGIN
		DELETE FROM invoices WHERE id = _id;
	END;
$$;


ALTER PROCEDURE public.delete_invoice(IN _id numeric) OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 216 (class 1259 OID 16421)
-- Name: invoices; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.invoices (
    id integer NOT NULL,
    name character varying(200) NOT NULL,
    date timestamp with time zone NOT NULL,
    amount character varying(200) NOT NULL,
    file character varying(50)
);


ALTER TABLE public.invoices OWNER TO postgres;

--
-- TOC entry 218 (class 1255 OID 16433)
-- Name: get_all_invoices(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.get_all_invoices() RETURNS SETOF public.invoices
    LANGUAGE plpgsql
    AS $$
	BEGIN
		return query SELECT * from invoices order by id;
	END;
$$;


ALTER FUNCTION public.get_all_invoices() OWNER TO postgres;

--
-- TOC entry 217 (class 1255 OID 16435)
-- Name: get_invoice_by_id(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.get_invoice_by_id(invoice_id integer) RETURNS SETOF public.invoices
    LANGUAGE plpgsql
    AS $$
	begin
		RETURN QUERY SELECT * from invoices where id = invoice_id;
	END;
$$;


ALTER FUNCTION public.get_invoice_by_id(invoice_id integer) OWNER TO postgres;

--
-- TOC entry 220 (class 1255 OID 16472)
-- Name: update_invoice(numeric, character varying, timestamp with time zone, character varying, character varying); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.update_invoice(IN _id numeric, IN _name character varying, IN _date timestamp with time zone, IN _amount character varying, IN _file character varying)
    LANGUAGE plpgsql
    AS $$
	BEGIN
	UPDATE invoices
	SET name = _name, 
		date = _date,
		amount = _amount,
		file = _file
	WHERE id = _id;
	END;
$$;


ALTER PROCEDURE public.update_invoice(IN _id numeric, IN _name character varying, IN _date timestamp with time zone, IN _amount character varying, IN _file character varying) OWNER TO postgres;

--
-- TOC entry 215 (class 1259 OID 16420)
-- Name: invoices_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.invoices_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.invoices_id_seq OWNER TO postgres;

--
-- TOC entry 4845 (class 0 OID 0)
-- Dependencies: 215
-- Name: invoices_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.invoices_id_seq OWNED BY public.invoices.id;


--
-- TOC entry 4693 (class 2604 OID 16424)
-- Name: invoices id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.invoices ALTER COLUMN id SET DEFAULT nextval('public.invoices_id_seq'::regclass);


--
-- TOC entry 4695 (class 2606 OID 16428)
-- Name: invoices invoices_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.invoices
    ADD CONSTRAINT invoices_pk PRIMARY KEY (id);


-- Completed on 2024-05-31 22:09:52

--
-- PostgreSQL database dump complete
--

-- Completed on 2024-05-31 22:09:52

--
-- PostgreSQL database cluster dump complete
--

