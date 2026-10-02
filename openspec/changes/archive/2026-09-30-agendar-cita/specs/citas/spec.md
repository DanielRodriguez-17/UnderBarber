# Spec Delta

## Purpose

Permite agendar una cita eligiendo un servicio base (corte, barba o combo),
añadir extras opcionales (lavado, tinte) y notificar al cliente por SMS. El
sistema debe ocultar la complejidad de construcción mediante patrones de diseño.

## ADDED Requirements

### Requirement: RF-01 Crear servicio por tipo
El sistema DEBE crear el servicio base según el tipo indicado ("corte",
"barba" o "combo") sin que el código cliente use `new` sobre las clases
concretas.

#### Scenario: Tipo válido
- **WHEN** se pide el servicio "corte"
- **THEN** se obtiene un servicio con descripción "Corte de cabello" y precio 20000

#### Scenario: Tipo inexistente
- **WHEN** se pide un tipo que no existe
- **THEN** el sistema DEBE lanzar `ArgumentException` con el mensaje "Servicio no existe"

### Requirement: RF-02 Precios base
Corte = 20000, Barba = 15000, Combo (Corte + Barba) = 30000.

#### Scenario: Precios base definidos
- **WHEN** se crean los servicios base "corte", "barba" y "combo"
- **THEN** sus precios son 20000, 15000 y 30000 respectivamente

### Requirement: RF-03 Extras apilables
El sistema DEBE permitir añadir lavado (+5000) y/o tinte (+25000) a cualquier
servicio. La descripción DEBE listar cada extra y el precio DEBE ser la suma
del servicio base más los extras.

#### Scenario: Servicio con extras
- **GIVEN** un servicio "corte"
- **WHEN** se solicita lavado y tinte
- **THEN** la descripción es "Corte de cabello + Lavado + Tinte" y el precio es 50000

### Requirement: RF-04 Notificación
Al confirmar la cita, el sistema DEBE notificar al cliente por SMS a través de
la interfaz `INotificador`, sin que el resto del sistema dependa del sistema
SMS antiguo.

#### Scenario: Notificación al confirmar cita
- **GIVEN** una cita agendada
- **WHEN** se confirma la cita
- **THEN** se envía un mensaje SMS al teléfono del cliente usando `INotificador`

### Requirement: RF-05 Punto de entrada único
El sistema DEBE ofrecer un único método `AgendarCita(cliente, telefono,
tipoServicio, lavado, tinte)` que haga todo el proceso: crear servicio,
aplicar extras, mostrar resumen y notificar.

#### Scenario: Punto de entrada único coordina el flujo
- **WHEN** se llama a `AgendarCita` con los parámetros del cliente
- **THEN** el proceso completo (crear servicio, aplicar extras, mostrar resumen
  y notificar) se ejecuta desde ese único método
