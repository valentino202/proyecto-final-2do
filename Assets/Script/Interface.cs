using UnityEngine;

public interface IDamageable
{
    void TakeDamage(int amount);
}
public interface IMovementBehavior
{
    void Move(Transform odj);
}
public interface IDisparo
{
    void Disparar();
    float TiempoEntreDisparos { get; }
}
