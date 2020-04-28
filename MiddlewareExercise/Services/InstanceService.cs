namespace MiddlewareExercise.Services
{
    public class InstanceService : IInstanceService
    {
        private static int instances = 0;

        public InstanceService()
        {
            instances++;
        }

        public int Instances => instances;
    }
}
