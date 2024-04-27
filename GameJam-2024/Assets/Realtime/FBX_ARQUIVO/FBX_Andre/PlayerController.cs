using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Velocidade de movimento
    public Animator animator; // Refer�ncia ao componente Animator

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        // Iniciar com a anima��o Idle
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

        // Verificar se o jogador est� se movendo para ativar a anima��o de correr
        if (movement.magnitude > 0)
        {
            // Ativar a anima��o de correr
            animator.SetBool("IsRunning", true);
            // Ativar a anima��o Idle
            animator.SetBool("IsIdle", false);

            // Rotacionar o jogador na dire��o do movimento
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
        else
        {
            // Desativar a anima��o de correr se o jogador n�o estiver se movendo
            animator.SetBool("IsRunning", false);
            // Ativar a anima��o Idle
            animator.SetBool("IsIdle", true);
        }
    }
}
