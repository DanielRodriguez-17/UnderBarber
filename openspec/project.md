# Contexto: Ejercicio de clase — Patrones de diseño

## Descripción
Ejercicio simple para explicar 4 patrones de diseño (1 creacional + 3
estructurales) usando como temática un mini-sistema de agendamiento de citas
en una barbería.

## Alcance
Solo agendar una cita (crear servicio, aplicar extras, notificar). No es una
app completa: es un ejemplo didáctico.

## Stack técnico
- C# 8.0 / .NET 8, aplicación de consola

## Convenciones
- Código en `src/`, especificación en `openspec/`.
- Flujo SDD: proposal → specs → design → tasks → código.

## Patrones usados
- **Creacional — Factory Method**: `ServicioFactory`.
- **Estructural — Decorator**: `ConLavado`, `ConTinte`.
- **Estructural — Adapter**: `AdaptadorSMS`.
- **Estructural — Facade**: `BarberiaFacade`.
