# citas Specification

## Purpose
Permite agendar una cita eligiendo un servicio base (corte, barba o combo),
añadir extras opcionales (lavado, tinte) y notificar al cliente por SMS. El
sistema debe ocultar la complejidad de construcción mediante patrones de diseño.

## Requirements

### Requirement: RF-01 Crear servicio por tipo
El sistema DEBE crear el servicio base según el tipo indicado ("corte",
"barba", "combo" o "cejas") sin que el código cliente use `new` sobre las
clases concretas.

#### Scenario: Tipo válido
- **WHEN** se pide el servicio "corte"
- **THEN** se obtiene un servicio con descripción "Corte de cabello" y precio 20000

#### Scenario: Tipo válido adicional
- **WHEN** se pide el servicio "cejas"
- **THEN** se obtiene un servicio con descripción "Diseño de cejas" y precio 10000

#### Scenario: Tipo inexistente
- **WHEN** se pide un tipo que no existe
- **THEN** el sistema DEBE lanzar `ArgumentException` con el mensaje "Servicio no existe"

#### Scenario: Variante no registrada
- **WHEN** se pide el tipo "ceja" en singular o "CEJAS" en mayúsculas
- **THEN** el sistema DEBE lanzar `ArgumentException` con el mensaje "Servicio no existe",
  porque la fábrica no normaliza ni hace coincidencia parcial

### Requirement: RF-02 Precios base
Corte = 20000, Barba = 15000, Combo (Corte + Barba) = 30000, Cejas = 10000.

#### Scenario: Precios base definidos
- **WHEN** se crean los servicios base "corte", "barba" y "combo"
- **THEN** sus precios son 20000, 15000 y 30000 respectivamente

#### Scenario: Precios de todos los servicios base
- **WHEN** se crea cada uno de los tipos registrados ("corte", "barba", "combo", "cejas")
- **THEN** sus precios son respectivamente 20000, 15000, 30000 y 10000

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

### Requirement: RF-06 Servicio Cejas
El sistema DEBE ofrecer "cejas" como servicio base del catálogo, con descripción
"Diseño de cejas" y precio 10000, alcanzable por el mismo punto de entrada que
los demás servicios.

#### Scenario: Cita de cejas sin extras
- **GIVEN** un cliente con teléfono registrado
- **WHEN** se agenda una cita con tipo "cejas", lavado = no y tinte = no
- **THEN** el resumen muestra "Diseño de cejas" y un precio total de 10000, y se
  envía la confirmación por SMS al teléfono del cliente

#### Scenario: Cita de cejas con extras
- **WHEN** se agenda una cita con tipo "cejas", lavado = sí y tinte = sí
- **THEN** la descripción es "Diseño de cejas + Lavado + Tinte" y el precio total
  es 40000, con el mismo orden de extras que en los demás servicios

#### Scenario: Cita de cejas con un solo extra
- **WHEN** se agenda una cita con tipo "cejas" y solo lavado
- **THEN** la descripción es "Diseño de cejas + Lavado" y el precio total es 15000

#### Scenario: El alta no altera los servicios existentes
- **WHEN** se agendan citas de "corte", "barba" y "combo" después de que exista
  el servicio "cejas"
- **THEN** sus descripciones, precios y notificación se mantienen sin cambios
