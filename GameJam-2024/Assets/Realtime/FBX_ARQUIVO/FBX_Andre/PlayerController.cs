using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Velocidade de movimento
    public Animator animator; // Referência ao componente Animator

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        // Iniciar com a animação Idle
        animator.SetBool("IsRunning", false);
    }

    void Update()
    {
        // Receber entrada do teclado
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaY = Input.GetAxis("Vertical") * speed;

        Vector3 movement = new Vector3(deltaX, rb.velocity.y, deltaY);
        movement = Vector3.ClampMagnitude(movement, speed);

        // Aplica o movimento ao Rigidbody
        rb.velocity = movement;

        // Verificar se o jogador está se movendo para ativar a animação de correr
        if (movement.magnitude > 0)
        {
            // Ativar a animação de correr
            animator.SetBool("IsRunning", true);
            // Ativar a animação Idle
            animator.SetBool("IsIdle", false);

            // Rotacionar o jogador na direção do movimento
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
        else
        {
            // Desativar a animação de correr se o jogador não estiver se movendo
            animator.SetBool("IsRunning", false);
            // Ativar a animação Idle
            animator.SetBool("IsIdle", true);
        }
    }
}
