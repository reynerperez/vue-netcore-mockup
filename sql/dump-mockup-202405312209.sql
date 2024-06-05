--
-- PostgreSQL database dump
--

-- Dumped from database version 16.3 (Postgres.app)
-- Dumped by pg_dump version 16.3 (Postgres.app)

-- Started on 2024-06-05 11:37:45 CST

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
-- TOC entry 4 (class 2615 OID 2200)
-- Name: public; Type: SCHEMA; Schema: -; Owner: pg_database_owner
--

CREATE SCHEMA public;


ALTER SCHEMA public OWNER TO pg_database_owner;

--
-- TOC entry 3624 (class 0 OID 0)
-- Dependencies: 4
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: pg_database_owner
--

COMMENT ON SCHEMA public IS 'standard public schema';


--
-- TOC entry 221 (class 1255 OID 16421)
-- Name: create_invoice(character varying, timestamp with time zone, character varying, character varying, numeric); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.create_invoice(IN _name character varying, IN _date timestamp with time zone, IN _amount character varying, IN _file character varying, INOUT _id numeric)
    LANGUAGE plpgsql
    AS $$
BEGIN
  INSERT INTO invoices (name, date, amount, file)
  VALUES (_name, _date, _amount, _file) returning id into _id;

END;
$$;


ALTER PROCEDURE public.create_invoice(IN _name character varying, IN _date timestamp with time zone, IN _amount character varying, IN _file character varying, INOUT _id numeric) OWNER TO postgres;

--
-- TOC entry 217 (class 1255 OID 16392)
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
-- TOC entry 215 (class 1259 OID 16393)
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
-- TOC entry 218 (class 1255 OID 16396)
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
-- TOC entry 219 (class 1255 OID 16397)
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
-- TOC entry 222 (class 1255 OID 16423)
-- Name: search_invoices(text); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.search_invoices(term text) RETURNS SETOF public.invoices
    LANGUAGE plpgsql
    AS $$
	BEGIN
		return query select * from invoices 
			where to_tsvector(lower(name)) @@ phraseto_tsquery(CONCAT (lower(term), ':*')) 
			or "name" like CONCAT ('%',lower(term),'%');
	END;
$$;


ALTER FUNCTION public.search_invoices(term text) OWNER TO postgres;

--
-- TOC entry 220 (class 1255 OID 16398)
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
-- TOC entry 216 (class 1259 OID 16399)
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
-- TOC entry 3625 (class 0 OID 0)
-- Dependencies: 216
-- Name: invoices_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.invoices_id_seq OWNED BY public.invoices.id;


--
-- TOC entry 3471 (class 2604 OID 16400)
-- Name: invoices id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.invoices ALTER COLUMN id SET DEFAULT nextval('public.invoices_id_seq'::regclass);


--
-- TOC entry 3617 (class 0 OID 16393)
-- Dependencies: 215
-- Data for Name: invoices; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.invoices (id, name, date, amount, file) FROM stdin;
200	Gasolina	2024-06-05 00:00:00-06	200	
201	Comida	2024-06-05 00:00:00-06	100	
202	Casa	2024-06-05 00:00:00-06	200	
203	Internet	2024-06-05 00:00:00-06	400	
204	Doctor 	2024-06-05 00:00:00-06	300	
205	Doctor 2	2024-06-05 00:00:00-06	100	
206	Doctor 3	2024-06-05 00:00:00-06	500	
\.


--
-- TOC entry 3626 (class 0 OID 0)
-- Dependencies: 216
-- Name: invoices_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.invoices_id_seq', 206, true);


--
-- TOC entry 3473 (class 2606 OID 16402)
-- Name: invoices invoices_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.invoices
    ADD CONSTRAINT invoices_pk PRIMARY KEY (id);


-- Completed on 2024-06-05 11:37:45 CST

--
-- PostgreSQL database dump complete
--

