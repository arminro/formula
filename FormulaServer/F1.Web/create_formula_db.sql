USE [master]
                                GO

                                /****** Object:  Database [FormulaDb]    Script Date: 2021. 11. 27. 20:54:34 ******/
                                CREATE DATABASE [FormulaDb]
                                 CONTAINMENT = NONE
                                 ON  PRIMARY 
                                ( NAME = N'FormulaDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\FormulaDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
                                 LOG ON 
                                ( NAME = N'FormulaDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\FormulaDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
                                 WITH CATALOG_COLLATION = DATABASE_DEFAULT
                                GO

                                IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
                                begin
                                EXEC [FormulaDb].[dbo].[sp_fulltext_database] @action = 'enable'
                                end
                                GO

                                ALTER DATABASE [FormulaDb] SET ANSI_NULL_DEFAULT OFF 
                                GO

                                ALTER DATABASE [FormulaDb] SET ANSI_NULLS OFF 
                                GO

                                ALTER DATABASE [FormulaDb] SET ANSI_PADDING OFF 
                                GO

                                ALTER DATABASE [FormulaDb] SET ANSI_WARNINGS OFF 
                                GO

                                ALTER DATABASE [FormulaDb] SET ARITHABORT OFF 
                                GO

                                ALTER DATABASE [FormulaDb] SET AUTO_CLOSE OFF 
                                GO

                                ALTER DATABASE [FormulaDb] SET AUTO_SHRINK OFF 
                                GO

                                ALTER DATABASE [FormulaDb] SET AUTO_UPDATE_STATISTICS ON 
                                GO

                                ALTER DATABASE [FormulaDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
                                GO

                                ALTER DATABASE [FormulaDb] SET CURSOR_DEFAULT  GLOBAL 
                                GO

                                ALTER DATABASE [FormulaDb] SET CONCAT_NULL_YIELDS_NULL OFF 
                                GO

                                ALTER DATABASE [FormulaDb] SET NUMERIC_ROUNDABORT OFF 
                                GO

                                ALTER DATABASE [FormulaDb] SET QUOTED_IDENTIFIER OFF 
                                GO

                                ALTER DATABASE [FormulaDb] SET RECURSIVE_TRIGGERS OFF 
                                GO

                                ALTER DATABASE [FormulaDb] SET  DISABLE_BROKER 
                                GO

                                ALTER DATABASE [FormulaDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
                                GO

                                ALTER DATABASE [FormulaDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
                                GO

                                ALTER DATABASE [FormulaDb] SET TRUSTWORTHY OFF 
                                GO

                                ALTER DATABASE [FormulaDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
                                GO

                                ALTER DATABASE [FormulaDb] SET PARAMETERIZATION SIMPLE 
                                GO

                                ALTER DATABASE [FormulaDb] SET READ_COMMITTED_SNAPSHOT OFF 
                                GO

                                ALTER DATABASE [FormulaDb] SET HONOR_BROKER_PRIORITY OFF 
                                GO

                                ALTER DATABASE [FormulaDb] SET RECOVERY SIMPLE 
                                GO

                                ALTER DATABASE [FormulaDb] SET  MULTI_USER 
                                GO

                                ALTER DATABASE [FormulaDb] SET PAGE_VERIFY CHECKSUM  
                                GO

                                ALTER DATABASE [FormulaDb] SET DB_CHAINING OFF 
                                GO

                                ALTER DATABASE [FormulaDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
                                GO

                                ALTER DATABASE [FormulaDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
                                GO

                                ALTER DATABASE [FormulaDb] SET DELAYED_DURABILITY = DISABLED 
                                GO

                                ALTER DATABASE [FormulaDb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
                                GO

                                ALTER DATABASE [FormulaDb] SET QUERY_STORE = OFF
                                GO

                                ALTER DATABASE [FormulaDb] SET  READ_WRITE 
                                GO