using System;
using UnityEngine;

public class SpringJoint2DProxy : MonoBehaviour
{
    // ��Ӧ Spring Joint 2D ������
    [Header("Spring Joint 2D Settings")]
    public Rigidbody2D connectedRigidbody; // ���ӵĸ��壬��Ӧ Connected Rigid Body
    public Vector2 anchor = Vector2.zero;   // ����ê�㣬��Ӧ Anchor
    public Vector2 connectedAnchor;         // ���Ӹ����ê�㣬��Ӧ Connected Anchor
    public float distance = 0.3f;           // ���ɾ��룬��Ӧ Distance��ԭ restLength ���������
    public float dampingRatio = 0f;         // ����ȣ���Ӧ Damping Ratio
    public float frequency = 2f;            // Ƶ�ʣ���Ӧ Frequency
    public BreakAction breakAction = BreakAction.Destroy; // ������Ϊ����Ӧ Break Action
    public float breakForce = Mathf.Infinity; // ����������Ӧ Break Force

    private Rigidbody2D selfRb;
    // �������Ӹ���ĳ�ʼ�ٶȣ���ѡ������������
    private Vector2 connectedInitialVelocity; 

    void Start()
    {
        selfRb = GetComponent<Rigidbody2D>();
        if (connectedRigidbody != null)
        {
            connectedInitialVelocity = connectedRigidbody.velocity;
        }
    }

    void FixedUpdate()
    {
        // ���ж��Ƿ���ѣ���ʾ����������󳬹��������߼���ʵ�ʿɸ����ӣ�
        float currentDistance = Vector2.Distance(
            selfRb.position + anchor, 
            (connectedRigidbody ? connectedRigidbody.position : Vector2.zero) + connectedAnchor
        );
        if (currentDistance > breakForce && breakAction != BreakAction.DoNothing)
        {
            HandleBreakAction();
            return; 
        }

        // ���㵯�������� Unity �����߼���ʾ����Ҫ Rust �����ⲿ���߼��� DLL ����ã�
        Vector2 force = CalculateSpringForce();
        selfRb.AddForce(force);

        // ��������Ӹ��壬Ҳ���Ը���������������������ʵ�ؽ����໥�ģ�
        if (connectedRigidbody != null)
        {
            connectedRigidbody.AddForce(-force);
        }
    }

    // ���㵯���������� Spring Joint 2D �����߼����򻯰棩
    private Vector2 CalculateSpringForce()
    {
        // 1. ��������ê�������λ��
        Vector2 selfAnchorWorld = selfRb.position + anchor;
        Vector2 connectedAnchorWorld = connectedRigidbody ? 
            connectedRigidbody.position + connectedAnchor : Vector2.zero;

        // 2. ���㵱ǰ���ȡ����λ��
        float currentLength = Vector2.Distance(selfAnchorWorld, connectedAnchorWorld);
        Vector2 displacement = selfAnchorWorld - connectedAnchorWorld;
        displacement = displacement.normalized * (currentLength - distance); // �α���

        // 3. ��������ٶȣ�����ê���ٶ� - ����ê���ٶȣ�
        Vector2 selfAnchorVelocity = selfRb.velocity;
        Vector2 connectedAnchorVelocity = connectedRigidbody ? 
            connectedRigidbody.velocity : Vector2.zero;
        Vector2 relativeVelocity = selfAnchorVelocity - connectedAnchorVelocity;

        // 4. ��Ƶ�ʡ�����Ȼ���նȺ����ᣨ�ο� Unity Spring Joint 2D ����ʽ��
        // ��ʽ�򻯣�k = (2��f)^2 * m ������ m ��ȡ 1 ��ʾ��ʵ�ʿ��ø�������
        float stiffness = Mathf.Pow(2 * Mathf.PI * frequency, 2); 
        float damping = 2 * Mathf.Sqrt(stiffness) * dampingRatio;

        // 5. ���˶��ɼ�������F = -k * �α���  - ���� * ����ٶ�
        Vector2 springForce = -stiffness * displacement - damping * relativeVelocity;

        return springForce;
    }

    // ���������Ϊ
    private void HandleBreakAction()
    {
        switch (breakAction)
        {
            case BreakAction.Destroy:
                Destroy(gameObject); // ��������
                if (connectedRigidbody != null)
                {
                    Destroy(connectedRigidbody.gameObject); // ��ѡ��Ҳ������������
                }
                break;
            case BreakAction.Disable:
                enabled = false; // �رսű���ֹͣ�����߼�
                break;
            case BreakAction.DoNothing:
                // ɶҲ�������������Զ�������߼�
                break;
        }
    }

    // ������Ϊö�٣���Ӧ Spring Joint 2D �� Break Action
    public enum BreakAction
    {
        Destroy,
        Disable,
        DoNothing
    }
}

