# Propuesta: Agendar cita

## 1. Problema
**Sistema:** gestión de citas de una barbería.
**Qué hace:** permite agendar una cita eligiendo un servicio base (corte,
barba o combo), añadir extras opcionales (lavado, tinte) y notificar al
cliente por SMS.
**Quién lo usa:** la recepcionista o el barbero (y a futuro una app para
clientes).
**Necesidad al crear objetos:** hay varios tipos de servicio con precio y
descripción distintos, los extras se pueden combinar libremente, y el envío de
SMS depende de un sistema antiguo con una interfaz incompatible. Crear todo
esto directamente con `new` y `if/else` en el código de la cita genera código
repetido, acoplado y difícil de extender (una clase por combinación de extras,
cambios en varios sitios al agregar un servicio).

## 2. Qué se construye
Un mini-flujo de consola que agenda citas aplicando 4 patrones de diseño.
Es un ejemplo didáctico, no una funcionalidad de producción.

## 3. Fuera de alcance
Persistencia, agenda de horarios, pagos, interfaz gráfica.
