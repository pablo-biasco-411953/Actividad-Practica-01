CREATE DATABASE ActividadPractica;
GO

USE ActividadPractica;
GO

CREATE TABLE FormaPago(
    id_formaPago INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL
);

CREATE TABLE Articulo(
    id_articulo INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    precioUnitario DECIMAL(10,2) NOT NULL
);

CREATE TABLE Factura(
    id_nroFactura INT IDENTITY(1,1) PRIMARY KEY,
    fecha DATE NOT NULL,
    id_formaPago INT NOT NULL,
    cliente VARCHAR(50) NOT NULL,
    CONSTRAINT fk_facturas_formaPago FOREIGN KEY (id_formaPago) REFERENCES FormaPago(id_formaPago)
);

CREATE TABLE DetalleFactura(
    id_detalle INT IDENTITY(1,1) PRIMARY KEY,
    id_nroFactura INT NOT NULL,
    id_articulo INT NOT NULL,
    cantidad INT NOT NULL,
    CONSTRAINT fk_detalle_factura FOREIGN KEY (id_nroFactura) REFERENCES Factura(id_nroFactura),
    CONSTRAINT fk_detalle_articulo FOREIGN KEY (id_articulo) REFERENCES Articulo(id_articulo),
    CONSTRAINT uq_factura_articulo UNIQUE (id_nroFactura, id_articulo)
);
GO

-- Procedimientos almacenados

USE [ActividadPractica]
GO
/****** Object:  StoredProcedure [dbo].[sp_Articulo_Insertar]    Script Date: 9/9/2025 21:30:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER   PROCEDURE [dbo].[sp_Articulo_Insertar]
    @nombre VARCHAR(100),
    @precioUnitario DECIMAL(10,2)
AS
BEGIN
    INSERT INTO Articulo (nombre, precioUnitario)
    VALUES (@nombre, @precioUnitario);
END;


USE [ActividadPractica]
GO
/****** Object:  StoredProcedure [dbo].[sp_Facturas_Listar]    Script Date: 9/9/2025 21:30:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER   PROCEDURE [dbo].[sp_Facturas_Listar]
AS
BEGIN
    SELECT f.id_nroFactura, f.fecha, f.cliente, fp.nombre AS FormaPago,
           SUM(a.precioUnitario * df.cantidad) AS Total
    FROM Factura f
    JOIN FormaPago fp ON f.id_formaPago = fp.id_formaPago
    LEFT JOIN DetalleFactura df ON f.id_nroFactura = df.id_nroFactura
    LEFT JOIN Articulo a ON df.id_articulo = a.id_articulo
    GROUP BY f.id_nroFactura, f.fecha, f.cliente, fp.nombre
    ORDER BY f.fecha DESC, f.id_nroFactura DESC;
END;


USE [ActividadPractica]
GO
/****** Object:  StoredProcedure [dbo].[sp_Factura_Insertar]    Script Date: 9/9/2025 23:12:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_Factura_Insertar]
    @fecha DATE,
    @id_formaPago INT,
    @cliente VARCHAR(50),
    @id_nroFactura INT OUTPUT
AS
BEGIN
    INSERT INTO Factura (fecha, id_formaPago, cliente)
    VALUES (@fecha, @id_formaPago, @cliente);

    SET @id_nroFactura = SCOPE_IDENTITY();
END;



USE [ActividadPractica]
GO
/****** Object:  StoredProcedure [dbo].[sp_DetalleFactura_Insertar]    Script Date: 9/9/2025 23:10:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[sp_DetalleFactura_Insertar]
    @id_nroFactura INT,
    @id_articulo INT,
    @cantidad INT
AS
BEGIN
    IF EXISTS (SELECT 1 
               FROM DetalleFactura 
               WHERE id_nroFactura = @id_nroFactura AND id_articulo = @id_articulo)
    BEGIN
        UPDATE DetalleFactura
        SET cantidad = cantidad + @cantidad
        WHERE id_nroFactura = @id_nroFactura AND id_articulo = @id_articulo;
    END
    ELSE
    BEGIN
        INSERT INTO DetalleFactura (id_nroFactura, id_articulo, cantidad)
        VALUES (@id_nroFactura, @id_articulo, @cantidad);
    END
END;



USE [ActividadPractica]
GO
/****** Object:  StoredProcedure [dbo].[sp_Facturas_Listar]    Script Date: 9/9/2025 23:11:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER   PROCEDURE [dbo].[sp_Facturas_Listar]
AS
BEGIN
    SELECT f.id_nroFactura, f.fecha, f.cliente, fp.nombre AS FormaPago,
           SUM(a.precioUnitario * df.cantidad) AS Total
    FROM Factura f
    JOIN FormaPago fp ON f.id_formaPago = fp.id_formaPago
    LEFT JOIN DetalleFactura df ON f.id_nroFactura = df.id_nroFactura
    LEFT JOIN Articulo a ON df.id_articulo = a.id_articulo
    GROUP BY f.id_nroFactura, f.fecha, f.cliente, fp.nombre
    ORDER BY f.fecha DESC, f.id_nroFactura DESC;
END;


USE [ActividadPractica]
GO
/****** Object:  StoredProcedure [dbo].[sp_FormaPago_Insertar]    Script Date: 9/9/2025 23:11:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER   PROCEDURE [dbo].[sp_FormaPago_Insertar]
    @nombre VARCHAR(50)
AS
BEGIN
    INSERT INTO FormaPago (nombre)
    VALUES (@nombre);
END;
